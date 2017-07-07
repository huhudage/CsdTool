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
    class ActionInfo
    {
        public String fileName;
        public String tag;
        public XmlNode actionNode;
        public XmlNode positionFrameNode;
    }

    /// <summary>
    /// CsdMerge 类，提供 csd 合并、统一缩放等功能支持
    /// 原始需求参见文档：http://10.2.48.228/pages/viewpage.action?pageId=511481038
    /// </summary>
    class CsdMerge
    {
        private const String contentPath = "GameProjectFile/Content/Content";
        private const String bodyRootPath = contentPath + "/ObjectData";
        private const String animationRootPath = contentPath + "/Animation";

        private Dictionary<String, List<ActionInfo>> fileName2ActionInfos = new Dictionary<string, List<ActionInfo>>();

        private String srcPath;
        private String outputPath;
        private String outputFileName;
        private String dstImagePath;
        private float  globalScale;

        public CsdMerge(String srcPath, String outputPath, String dstImagePath, float scale)
        {
            this.srcPath = srcPath;
            this.outputPath = outputPath;
            this.dstImagePath = dstImagePath;
            this.globalScale = scale;

            outputFileName = Path.GetFileNameWithoutExtension(outputPath);
        }

        public void Merge()
        {
            // 检查 other 目录
            String otherDir = Path.Combine(srcPath, "../../other");
            if (!Directory.Exists(otherDir))
            {
                MessageBox.Show("找不到 other 目录，请重新设置 csd 路径");
                return;
            }

            // 读取主 csd 文件
            XmlDocument mainCsd = new XmlDocument();
            mainCsd.Load(srcPath);

            // 解析主 csd 文件
            ParseMainCsd(mainCsd);

            // 合并子 csd 文件
            String[] files = Directory.GetFiles(otherDir, "*.csd");
            foreach (String file in files)
            {
                MergeSubCsd(mainCsd, file);
            }

            // 更新 csd 名称
            XmlNode node = mainCsd.SelectSingleNode("GameProjectFile/Content/Content/ObjectData");
            SetAttribute(node, "Name", outputFileName);

            // 如果是冈布奥 csd，需要将初始位置强制设置成 (-200, 127)
            if (outputFileName.StartsWith("slime_") || outputFileName.StartsWith("costume_slime_"))
            {
                SetChildAttribute(node, "Position", "X", "-200");
                SetChildAttribute(node, "Position", "Y", "127");
            }

            // 处理统一缩放
            if (globalScale != 1.0f)
                ApplyGlobalScale(mainCsd);

            // 保存合并后的 csd 文件
            mainCsd.Save(outputPath);

            // 打开目标文件夹
            System.Diagnostics.Process.Start("Explorer", Path.GetDirectoryName(outputPath));
        }

        // 解析主 csd 文件
        private void ParseMainCsd(XmlDocument mainCsd)
        {
            Dictionary<String, ActionInfo> tag2ActionInfo = new Dictionary<string, ActionInfo>();

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
                    String tag = element.GetAttribute("ActionTag");
                    String fileName = Path.GetFileName(element.GetAttribute("fileName"));

                    if (!fileName2ActionInfos.ContainsKey(fileName))
                        fileName2ActionInfos[fileName] = new List<ActionInfo>();

                    // 缓存 fileName - List<ActionInfo> 映射
                    ActionInfo actionInfo = new ActionInfo();
                    actionInfo.fileName = fileName;
                    actionInfo.tag = tag;
                    actionInfo.actionNode = csdNode;

                    fileName2ActionInfos[fileName].Add(actionInfo);
                    tag2ActionInfo[tag] = actionInfo;
                }

                // 遍历 Animation 下所有的 Timeline 节点
                XmlNode animationNode = mainCsd.SelectSingleNode(animationRootPath);
                foreach (XmlNode timelineNode in animationNode)
                {
                    XmlElement element = (XmlElement)timelineNode;
                    String actionTag = element.GetAttribute("ActionTag");

                    if (!tag2ActionInfo.ContainsKey(actionTag))
                        continue;

                    ActionInfo csdInfo = tag2ActionInfo[actionTag];
                    String frameType = element.GetAttribute("FrameType");
                    if (frameType.Equals("PositionFrame"))
                    {
                        csdInfo.positionFrameNode = timelineNode;
                    }
                }
            }
        }

        // 合并子 csd
        private void MergeSubCsd(XmlDocument mainCsd, String csdFile)
        {
            String fileName = Path.GetFileName(csdFile);

            XmlDocument subCsd = new XmlDocument();
            subCsd.Load(csdFile);

            XmlNode layerNode = subCsd.SelectSingleNode("GameProjectFile/Content/Content/ObjectData/Children/NodeObjectData");
            XmlNode spriteNode = layerNode.SelectSingleNode("Children").ChildNodes[0];

            // 获取子 csd 的图片名称
            String imageName = Path.GetFileName(GetAttribute(spriteNode, "fileName"));
            String imagePath = String.Format("images/{0}/{1}/{2}", dstImagePath, outputFileName, imageName);

            // 获取子 csd 的相对位置
            String posX = GetChildAttribute(layerNode, "Position", "X");
            String posY = GetChildAttribute(layerNode, "Position", "Y");

            // 获取子 csd 的缩放
            String scaleX = GetChildAttribute(layerNode, "Scale", "ScaleX");
            String scaleY = GetChildAttribute(layerNode, "Scale", "ScaleY");

            // 获取子 csd 的形变
            String skewX = GetAttribute(layerNode, "RotationSkewX");
            String skewY = GetAttribute(layerNode, "RotationSkewY");

            // 获取子 csd 的尺寸
            String width = GetChildAttribute(spriteNode, "Size", "X");
            String height = GetChildAttribute(spriteNode, "Size", "Y");

            foreach (ActionInfo csdInfo in fileName2ActionInfos[fileName])
            {
                // Step1: 更新Action

                XmlNode node = csdInfo.actionNode;

                // 更新 Name 属性
                SetAttribute(node, "Name", GetAttribute(spriteNode, "Name"));

                // 更新 fileName 属性
                SetAttribute(node, "fileName", Path.GetFileName(GetAttribute(spriteNode, "fileName")));

                // 更新 RotationSkewX/RotationSkewY 属性
                SetAttribute(node, "RotationSkewX", skewX);
                SetAttribute(node, "RotationSkewY", skewY);

                // 更新 CanEdit 属性
                SetAttribute(node, "CanEdit", GetAttribute(spriteNode, "CanEdit"));

                // 更新 ctype 属性
                SetAttribute(node, "ctype", GetAttribute(spriteNode, "ctype"));

                // 更新 FileData/Path
                SetChildAttribute(node, "FileData", "Path", imagePath);

                // 更新 Position/X & Position/Y
                SetChildAttribute(node, "Position", "X", "0");
                SetChildAttribute(node, "Position", "Y", "0");

                // 更新 Scale/X & Scale/Y
                SetChildAttribute(node, "Scale", "ScaleX", scaleX);
                SetChildAttribute(node, "Scale", "ScaleY", scaleY);

                // 添加 Size 节点
                AddChildNode(mainCsd, node, "Size");
                SetChildAttribute(node, "Size", "X", width);
                SetChildAttribute(node, "Size", "Y", height);

                // Step2: 更新 Timeline/PositionFrame
                XmlNode positionFrameNode = csdInfo.positionFrameNode;
                foreach (XmlNode pointFrame in positionFrameNode)
                {
                    float x = Convert.ToSingle(GetAttribute(pointFrame, "X"));
                    float y = Convert.ToSingle(GetAttribute(pointFrame, "Y"));

                    float fixX = x + Convert.ToSingle(posX);
                    float fixY = y + Convert.ToSingle(posY);

                    SetAttribute(pointFrame, "X", fixX.ToString("0.000"));
                    SetAttribute(pointFrame, "Y", fixY.ToString("0.000"));
                }
            }
        }

        // 应用全局缩放
        private void ApplyGlobalScale(XmlDocument mainCsd)
        {
            XmlNode rootNode = mainCsd.SelectSingleNode(bodyRootPath);

            // Step1: 缩放模型

            // 缩放根节点位置
            ApplyScale(rootNode, "Position", globalScale);

            foreach (XmlNode partNode in rootNode.SelectSingleNode("Children"))
            {
                // 缩放部位节点位置
                ApplyScale(partNode, "Position", globalScale);

                if (partNode.SelectSingleNode("Children") == null)
                    continue;

                foreach (XmlNode spriteNode in partNode.SelectSingleNode("Children"))
                {
                    // 缩放精灵节点位置
                    ApplyScale(spriteNode, "Position", globalScale);

                    // 缩放精灵节点大小
                    ApplyScale(spriteNode, "Size", globalScale);
                }
            }

            // Step2: 缩放动画
            XmlNode animationNode = mainCsd.SelectSingleNode(animationRootPath);
            foreach (XmlNode timelineNode in animationNode)
            {
                String frameType = GetAttribute(timelineNode, "FrameType");
                if (!frameType.Equals("PositionFrame"))
                    continue;

                ApplyScale(timelineNode, "PointFrame", globalScale);
            }
        }

        // 设置节点属性
        private void SetAttribute(XmlNode node, String name, String value)
        {
            XmlElement element = (XmlElement)node;
            element.SetAttribute(name, value);
        }

        // 获取节点属性
        private String GetAttribute(XmlNode node, String name)
        {
            XmlElement element = (XmlElement)node;
            return element.GetAttribute(name);
        }

        // 设置子节点属性
        private void SetChildAttribute(XmlNode node, String childName, String name, String value)
        {
            XmlNode childNode = node.SelectSingleNode(childName);
            SetAttribute(childNode, name, value);
        }

        // 获取子节点属性
        private String GetChildAttribute(XmlNode node, String childName, String name)
        {
            XmlNode childNode = node.SelectSingleNode(childName);
            return GetAttribute(childNode, name);
        }

        // 添加子节点
        private void AddChildNode(XmlDocument doc, XmlNode node, String childName)
        {
            XmlElement element = (XmlElement)node;
            XmlElement childElement = doc.CreateElement(childName);
            element.AppendChild(childElement);
        }

        // 子节点应用缩放
        private void ApplyScale(XmlNode node, string childName, float scale)
        {
            float x = Convert.ToSingle(GetChildAttribute(node, childName, "X"));
            float y = Convert.ToSingle(GetChildAttribute(node, childName, "Y"));

            float scaledX = x * scale;
            float scaledY = y * scale;

            SetChildAttribute(node, childName, "X", scaledX.ToString());
            SetChildAttribute(node, childName, "Y", scaledY.ToString());
        }
    }
}
