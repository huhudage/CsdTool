using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;

namespace CsdMergeTool
{
    class CsdInfo
    {
        public String fileName;
        public String actionTag;
        public XmlNode csdNode;
        public XmlNode positionFrame;
    }

    class CsdMerge
    {
        private Dictionary<String, List<CsdInfo>> fileName2Csd = new Dictionary<string, List<CsdInfo>>();
        private Dictionary<String, CsdInfo> actionTag2Csd = new Dictionary<string, CsdInfo>();

        private String rootPath;
        private String outputPath;

        public CsdMerge(String srcDir, String outputDir)
        {
            rootPath = Path.Combine(srcDir, "CocosStudio");

            String pureFileName = Path.GetFileNameWithoutExtension(srcDir);
            outputPath = Path.Combine(outputDir, pureFileName + ".csd");
        }

        public void Merge()
        {
            // 检查 csd 路径
            if (!Directory.Exists(rootPath))
            {
                MessageBox.Show("找不到 CocosStudio 目录，请重新设置源CSD路径");
                return;
            }

            // 找到 mc 目录
            String mcDir = "";
            String[] dirs = Directory.GetDirectories(rootPath);
            foreach (String dir in dirs)
            {
                String pureDir = Path.GetFileNameWithoutExtension(dir);
                if (pureDir.Equals("mc"))
                {
                    mcDir = dir;
                    break;
                }
            }

            if (String.IsNullOrEmpty(mcDir))
            {
                MessageBox.Show("找不到 mc 目录，请重新设置源CSD路径");
                return;
            }
            
            String[] files = Directory.GetFiles(mcDir);
            if (files.Length == 0)
            {
                MessageBox.Show("mc 目录为空，请重新设置源CSD路径");
                return;
            }

            // 读取主 csd 文件
            String mcFile = files[0];
            XmlDocument mainCsd = new XmlDocument();
            mainCsd.Load(mcFile);

            // 解析主 csd 文件
            ParseMainCsd(mainCsd);

            // 合并子 csd 文件
            foreach (String csdFileName in fileName2Csd.Keys)
            {
                MergeSubCsd(csdFileName);
            }

            // 保存合并后的 csd 文件
            mainCsd.Save(outputPath);
        }

        // 解析主 csd 文件
        private void ParseMainCsd(XmlDocument mainCsd)
        {
            String contentPath = "GameProjectFile/Content/Content";
            String bodyRootPath = contentPath + "/ObjectData";
            String animationRootPath = contentPath + "/Animation";

            // 遍历 body 根节点下的所有部位，如眼睛、嘴巴、武器等等
            XmlNode rootNode = mainCsd.SelectSingleNode(bodyRootPath + "/Children");
            foreach (XmlNode partNode in rootNode)
            {
                Log.d(String.Format("parse part {0}", ((XmlElement)partNode).GetAttribute("Name")));

                // 遍历某个部位下嵌入的所有子 csd 节点
                if (partNode.SelectSingleNode("Children") == null)
                    continue;

                foreach (XmlNode csdNode in partNode.SelectSingleNode("Children"))
                {
                    XmlElement element = (XmlElement)csdNode;
                    String actionTag = element.GetAttribute("ActionTag");
                    String fileName = element.GetAttribute("fileName");

                    // 一个 csd 可能被引入多次
                    if (!fileName2Csd.ContainsKey(fileName))
                        fileName2Csd[fileName] = new List<CsdInfo>();

                    // 缓存 fileName - List<CsdInfo> 映射
                    CsdInfo csdInfo = new CsdInfo();
                    csdInfo.fileName = fileName;
                    csdInfo.actionTag = actionTag;
                    csdInfo.csdNode = csdNode;

                    fileName2Csd[fileName].Add(csdInfo);
                    actionTag2Csd[actionTag] = csdInfo;
                }

                // 遍历 Animation 下所有的 Timeline 节点
                XmlNode animationNode = mainCsd.SelectSingleNode(animationRootPath);
                foreach (XmlNode timelineNode in animationNode)
                {
                    XmlElement element = (XmlElement)timelineNode;
                    String actionTag = element.GetAttribute("ActionTag");

                    if (!actionTag2Csd.ContainsKey(actionTag))
                        continue;

                    CsdInfo csdInfo = actionTag2Csd[actionTag];
                    String frameType = element.GetAttribute("FrameType");
                    if (frameType.Equals("PositionFrame"))
                    {
                        csdInfo.positionFrame = timelineNode;
                    }
                }
            }
        }

        // 合并子 csd
        private void MergeSubCsd(String csdFileName)
        {
            String subCsdPath = Path.Combine(rootPath, csdFileName);
            if (!File.Exists(subCsdPath))
            {
                MessageBox.Show("找不到 csd 文件:" + csdFileName);
                return;
            }

            XmlDocument subCsd = new XmlDocument();
            subCsd.Load(subCsdPath);

            XmlElement layerNode = (XmlElement)subCsd.SelectSingleNode("GameProjectFile/Content/Content/ObjectData/Children/NodeObjectData");
            XmlElement spriteNode = (XmlElement)layerNode.SelectSingleNode("Children").ChildNodes[0];

            String fileName = spriteNode.GetAttribute("fileName");
            String pureFileName = Path.GetFileName(fileName);

            foreach (CsdInfo csdInfo in fileName2Csd[csdFileName])
            {
                XmlNode node = csdInfo.csdNode;
                XmlElement fileDataElement = (XmlElement)node.SelectSingleNode("FileData");
                fileDataElement.SetAttribute("Path", pureFileName);
            }

            Console.WriteLine("absorb " + subCsd.Name);
        }
    }
}
