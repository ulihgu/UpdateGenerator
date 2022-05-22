using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;
namespace UpdateGenerator
{
 public class XMLProcess  
    {  
        #region ���캯��  
        public XMLProcess()  
        { }
        const byte MAGICNUMBER = 142;
        public XMLProcess(string strPath)  
        {  
            this._XMLPath = strPath;
            if (!File.Exists(_XMLPath))
            {
                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "GB2312", null);
                    xmlDoc.AppendChild(node);
                    //�����Ԫ��
                    XmlElement root = xmlDoc.CreateElement("updateinformations");
                    xmlDoc.AppendChild(root);
                   // XmlNode FileName = xmlDoc.CreateNode(XmlNodeType.Element, "a.txt", null);
                   // root.AppendChild(FileName);
                   // CreateNode(xmlDoc, FileName, "FilePath", "");
                   // CreateNode(xmlDoc, FileName, "CreationTime", "11:11:11");
                   // CreateNode(xmlDoc, FileName, "ModificationTime", "12:12:12");
                   // CreateNode(xmlDoc, FileName, "AccessTime", "13:13:13");
                   // CreateNode(xmlDoc, FileName, "FileInformation", "");

                    xmlDoc.Save("update.xml");

                    
                }
                catch (Exception err)
                {
                    MessageBox.Show("ʧ�ܣ�"+err);
                    //SaMainProgram.UpDataLog("����XML�ļ�ʧ�ܣ�" + err);
               }

            }
            else
            {
                try
                {
                    System.IO.File.Delete(_XMLPath);

                    XmlDocument xmlDoc = new XmlDocument();
                    XmlNode node = xmlDoc.CreateXmlDeclaration("1.0", "GB2312", null);
                    xmlDoc.AppendChild(node);
                    //�����Ԫ��
                    XmlElement root = xmlDoc.CreateElement("updateinformations");
                    xmlDoc.AppendChild(root);
                    // XmlNode FileName = xmlDoc.CreateNode(XmlNodeType.Element, "a.txt", null);
                    // root.AppendChild(FileName);
                    // CreateNode(xmlDoc, FileName, "FilePath", "");
                    // CreateNode(xmlDoc, FileName, "CreationTime", "11:11:11");
                    // CreateNode(xmlDoc, FileName, "ModificationTime", "12:12:12");
                    // CreateNode(xmlDoc, FileName, "AccessTime", "13:13:13");
                    // CreateNode(xmlDoc, FileName, "FileInformation", "");

                    xmlDoc.Save("update.xml");
                }catch(Exception err)
                {
                    MessageBox.Show("ʧ�ܣ�" + err);
                }
            }
            XMLLoad("update.xml");
        }  
        #endregion  
 
        #region ��������  
        private string _XMLPath;  
        public string XMLPath  
        {  
            get { return this._XMLPath; }  
        }  
        #endregion  

        /// <summary>      
        /// �����ڵ�      
        /// </summary>      
        /// <param name="xmldoc"></param>  xml�ĵ�    
        /// <param name="parentnode"></param>���ڵ�      
        /// <param name="name"></param>  �ڵ���    
        /// <param name="value"></param>  �ڵ�ֵ    
        ///     
        public void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
           parentNode.AppendChild(node);
        } 
 
        #region ˽�з���  
        /// <summary>  
        /// ����XML�ļ�  
        /// </summary>  
        /// <param name="XMLPath">XML�ļ�·��</param>  
        private XmlDocument XMLLoad()  
        {  
            string XMLFile = XMLPath;  
            XmlDocument xmldoc = new XmlDocument();  
            try  
            {  
                string filename = AppDomain.CurrentDomain.BaseDirectory.ToString() + XMLFile;  
                if (File.Exists(filename)) xmldoc.Load(filename);  
            }  
            catch (Exception e)  
            {
                MessageBox.Show("����XMLʧ��e��" + e);
            }  
            return xmldoc;  
        }  
  
        /// <summary>  
        /// ����XML�ļ�  
        /// </summary>  
        /// <param name="XMLPath">XML�ļ�·��</param>  
        private static XmlDocument XMLLoad(string strPath)  
        {
           // MessageBox.Show("1�ɹ���");
            XmlDocument xmldoc = new XmlDocument();  
            try  
            {
                //MessageBox.Show("2�ɹ���");
                string filename = AppDomain.CurrentDomain.BaseDirectory.ToString() + strPath;  
                if (File.Exists(filename)) xmldoc.Load(filename);                
            }  
            catch (Exception e)  
            {
                MessageBox.Show("����XMLʧ��e��" + e);
            }  
            return xmldoc;  
        }  
  
        /// <summary>  
        /// ��������·��  
        /// </summary>  
        /// <param name="strPath">Xml��·��</param>  
        private static string GetXmlFullPath(string strPath)  
        {  
            if (strPath.IndexOf(":") > 0)  
            {  
                return strPath;  
            }  
            else  
            {
                return strPath;//System.Web.HttpContext.Current.Server.MapPath(strPath);  
            }  
        }  
        #endregion  
 
        #region ��ȡ����  
        /// <summary>  
        /// ��ȡָ���ڵ������  
        /// </summary>  
        /// <param name="node">�ڵ�</param>  
        /// ʹ��ʾ��:  
        /// XMLProsess.Read("/Node", "")  
        /// XMLProsess.Read("/Node/Element[@Attribute='Name']")  
        public string Read(string node)  
        {  
            string value = "";  
            try  
            {  
                XmlDocument doc = XMLLoad();  
                XmlNode xn = doc.SelectSingleNode(node);  
                value = xn.InnerText;  
                //XmlNodeList xmlxnl = doc.SelectNodes(node);
                //value = xmlxnl.Item(0).InnerXml;
            }  
            catch { }  
            return value;  
        }  
  
        /// <summary>  
        /// ��ȡָ��·���ͽڵ�Ĵ���ֵ  
        /// </summary>  
        /// <param name="path">·��</param>  
        /// <param name="node">�ڵ�</param>  
        /// <param name="attribute">���������ǿ�ʱ���ظ�����ֵ�����򷵻ش���ֵ</param>  
        /// ʹ��ʾ��:  
        /// XMLProsess.Read(path, "/Node", "")  
        /// XMLProsess.Read(path, "/Node/Element[@Attribute='Name']")  
        public static string Read(string path, string node)  
        {  
            string value = "";  
            try  
            {  
                XmlDocument doc = XMLLoad(path);  
                XmlNode xn = doc.SelectSingleNode(node);  
                value = xn.InnerText;  
            }  
            catch { }  
            return value;  
        }  
  
        /// <summary>  
        /// ��ȡָ��·���ͽڵ������ֵ  
        /// </summary>  
        /// <param name="path">·��</param>  
        /// <param name="node">�ڵ�</param>  
        /// <param name="attribute">���������ǿ�ʱ���ظ�����ֵ�����򷵻ش���ֵ</param>  
        /// ʹ��ʾ��:  
        /// XMLProsess.Read(path, "/Node", "")  
        /// XMLProsess.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")  
        public static string Read(string path, string node, string attribute)  
        {  
            string value = "";  
            try  
            {  
                XmlDocument doc = XMLLoad(path);  
                XmlNode xn = doc.SelectSingleNode(node);  
                value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);  
            }  
            catch { }  
            return value;  
        }  
  
        /// <summary>  
        /// ��ȡĳһ�ڵ�����к��ӽڵ��ֵ  
        /// </summary>  
        /// <param name="node">Ҫ��ѯ�Ľڵ�</param>  
        public string[] ReadAllChildallValue(string node)  
        {  
            int i = 0;  
            string[] str = { };  
            XmlDocument doc = XMLLoad();  
            XmlNode xn = doc.SelectSingleNode(node);  
            XmlNodeList nodelist = xn.ChildNodes;  //�õ��ýڵ���ӽڵ�  
            if (nodelist.Count > 0)  
            {  
                str = new string[nodelist.Count];  
                foreach (XmlElement el in nodelist)//��Ԫ��ֵ  
                {  
                    str[i] = el.Value;  
                    i++;  
                }  
            }  
            return str;  
        }  
  
        /// <summary>  
        /// ��ȡĳһ�ڵ�����к��ӽڵ��ֵ  
        /// </summary>  
        /// <param name="node">Ҫ��ѯ�Ľڵ�</param>  
        public XmlNodeList ReadAllChild(string node)  
        {  
            XmlDocument doc = XMLLoad();  
            XmlNode xn = doc.SelectSingleNode(node);  
            XmlNodeList nodelist = xn.ChildNodes;  //�õ��ýڵ���ӽڵ�  
            return nodelist;  
        }  
  
        /// <summary>   
        /// ��ȡXML���ؾ������ɸѡ���DataView  
        /// </summary>  
        /// <param name="strWhere">ɸѡ��������:"name='kgdiwss'"</param>  
        /// <param name="strSort"> ������������:"Id desc"</param>  
        public DataView GetDataViewByXml(string strWhere, string strSort)  
        {  
            try  
            {  
                string XMLFile = this.XMLPath;  
                string filename = AppDomain.CurrentDomain.BaseDirectory.ToString() + XMLFile;  
                DataSet ds = new DataSet();  
                ds.ReadXml(filename);  
                DataView dv = new DataView(ds.Tables[0]); //����DataView����������ɸѡ����      
                if (strSort != null)  
                {  
                    dv.Sort = strSort; //��DataView�еļ�¼��������  
                }  
                if (strWhere != null)  
                {  
                    dv.RowFilter = strWhere; //��DataView�еļ�¼����ɸѡ���ҵ�������Ҫ�ļ�¼  
                }  
                return dv;  
            }  
            catch (Exception)  
            {  
                return null;  
            }  
        }  
  
        /// <summary>  
        /// ��ȡXML����DataSet  
        /// </summary>  
        /// <param name="strXmlPath">XML�ļ����·��</param>  
        public DataSet GetDataSetByXml(string strXmlPath)  
        {  
            try  
            {  
                DataSet ds = new DataSet();  
                ds.ReadXml(GetXmlFullPath(strXmlPath));  
                if (ds.Tables.Count > 0)  
                {  
                    return ds;  
                }  
                return null;  
            }  
            catch (Exception)  
            {  
                return null;  
            }  
        }  
        #endregion  
 
        #region ��������  
        /// <summary>  
        /// ��������  
        /// </summary>  
        /// <param name="path">·��</param>  
        /// <param name="node">�ڵ�</param>  
        /// <param name="element">Ԫ�������ǿ�ʱ������Ԫ�أ������ڸ�Ԫ���в�������</param>  
        /// <param name="attribute">���������ǿ�ʱ�����Ԫ������ֵ���������Ԫ��ֵ</param>  
        /// <param name="value">ֵ</param>  
        /// ʹ��ʾ��:  
        /// XMLProsess.Insert(path, "/Node", "Element", "", "Value")  
        /// XMLProsess.Insert(path, "/Node", "Element", "Attribute", "Value")  
        /// XMLProsess.Insert(path, "/Node", "", "Attribute", "Value")  
        public static void Insert(string path, string node, string element, string attribute, string value)  
        {  
            try  
            {  
                XmlDocument doc = new XmlDocument();  
                doc.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);  
                XmlNode xn = doc.SelectSingleNode(node);  
                if (element.Equals(""))  
                {  
                    if (!attribute.Equals(""))  
                    {  
                        XmlElement xe = (XmlElement)xn;  
                        xe.SetAttribute(attribute, value);  
                    }  
                }  
                else  
                {  
                    XmlElement xe = doc.CreateElement(element);  
                    if (attribute.Equals(""))  
                        xe.InnerText = value;  
                    else  
                        xe.SetAttribute(attribute, value);  
                    xn.AppendChild(xe);  
                }  
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);  
            }  
            catch(Exception err) {
                MessageBox.Show("����XMLʧ�ܣ�"+err);
            }  
        }  
  
        /// <summary>  
        /// ��������  
        /// </summary>  
        /// <param name="path">·��</param>  
        /// <param name="node">�ڵ�</param>  
        /// <param name="element">Ԫ�������ǿ�ʱ������Ԫ�أ������ڸ�Ԫ���в�������</param>  
        /// <param name="strList">��XML��������ֵ��ɵĶ�ά����</param>  
        public static void Insert(string path, string node, string element, string[][] strList)  
        {  
            try  
            {  
                XmlDocument doc = new XmlDocument();  
                doc.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);  
                XmlNode xn = doc.SelectSingleNode(node);  
                XmlElement xe = doc.CreateElement(element);  
                string strAttribute = "";  
                string strValue = "";  
                for (int i = 0; i < strList.Length; i++)  
                {  
                    for (int j = 0; j < strList[i].Length; j++)  
                    {  
                        if (j == 0)  
                            strAttribute = strList[i][j];  
                        else  
                            strValue = strList[i][j];  
                    }  
                    if (strAttribute.Equals(""))  
                        xe.InnerText = strValue;  
                    else  
                        xe.SetAttribute(strAttribute, strValue);  
                }  
                xn.AppendChild(xe);  
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);  
            }  
            catch { }  
        }  
  
        /// <summary>  
        /// ����һ������  
        /// </summary>  
        /// <param name="strXmlPath">XML�ļ����·��</param>  
        /// <param name="Columns">Ҫ�����е��������飬�磺string[] Columns = {"name","IsMarried"};</param>  
        /// <param name="ColumnValue">Ҫ������ÿ�е�ֵ���飬�磺string[] ColumnValue={"XML��ȫ","false"};</param>  
        /// <returns>�ɹ�����true,���򷵻�false</returns>  
        public static bool WriteXmlByDataSet(string strXmlPath, string[] Columns, string[] ColumnValue)  
        {  
            try  
            {  
                //���ݴ����XML·���õ�.XSD��·���������ļ�����ͬһ��Ŀ¼��  
                string strXsdPath = strXmlPath.Substring(0, strXmlPath.IndexOf(".")) + ".xsd";  
                DataSet ds = new DataSet();  
                ds.ReadXmlSchema(GetXmlFullPath(strXsdPath)); //��XML�ܹ�����ϵ���е���������  
                ds.ReadXml(GetXmlFullPath(strXmlPath));  
                DataTable dt = ds.Tables[0];  
                DataRow newRow = dt.NewRow();                 //��ԭ���ı������ϴ�������  
                for (int i = 0; i < Columns.Length; i++)      //ѭ����һ���еĸ����и�ֵ  
                {  
                    newRow[Columns[i]] = ColumnValue[i];  
                }  
                dt.Rows.Add(newRow);  
                dt.AcceptChanges();  
                ds.AcceptChanges();  
                ds.WriteXml(GetXmlFullPath(strXmlPath));  
                return true;  
            }  
            catch (Exception)  
            {  
                return false;  
            }  
        }  
        #endregion  
 
        #region �޸�����  
        /// <summary>  
        /// �޸�ָ���ڵ������  
        /// </summary>  
        /// <param name="node">�ڵ�</param>  
        /// <param name="value">ֵ</param>  
        public void Update(string node, string value)  
        {  
            try  
            {  
                XmlDocument doc = XMLLoad();  
                XmlNode xn = doc.SelectSingleNode(node);  
                xn.InnerText = value;  
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + XMLPath);  
            }  
            catch { }  
        }  
  
        /// <summary>  
        /// �޸�ָ���ڵ������  
        /// </summary>  
        /// <param name="path">·��</param>  
        /// <param name="node">�ڵ�</param>  
        /// <param name="value">ֵ</param>  
        /// ʹ��ʾ��:  
        /// XMLProsess.Insert(path, "/Node","Value")  
        /// XMLProsess.Insert(path, "/Node","Value")  
        public static void Update(string path, string node, string value)  
        {  
            try  
            {  
                XmlDocument doc = XMLLoad(path);  
                XmlNode xn = doc.SelectSingleNode(node);  
                xn.InnerText = value;  
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);  
            }  
            catch { }  
        }  
  
        /// <summary>  
        /// �޸�ָ���ڵ������ֵ(��̬)  
        /// </summary>  
        /// <param name="path">·��</param>  
        /// <param name="node">�ڵ�</param>  
        /// <param name="attribute">���������ǿ�ʱ�޸ĸýڵ�����ֵ�������޸Ľڵ�ֵ</param>  
        /// <param name="value">ֵ</param>  
        /// ʹ��ʾ��:  
        /// XMLProsess.Insert(path, "/Node", "", "Value")  
        /// XMLProsess.Insert(path, "/Node", "Attribute", "Value")  
        public static void Update(string path, string node, string attribute, string value)  
        {  
            try  
            {  
                XmlDocument doc = XMLLoad(path);  
                XmlNode xn = doc.SelectSingleNode(node);  
                XmlElement xe = (XmlElement)xn;  
                if (attribute.Equals(""))  
                    xe.InnerText = value;  
                else  
                    xe.SetAttribute(attribute, value);  
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);  
            }  
            catch { }  
        }  
  
        /// <summary>  
        /// ���ķ���������һ����¼  
        /// </summary>  
        /// <param name="strXmlPath">XML�ļ�·��</param>  
        /// <param name="Columns">��������</param>  
        /// <param name="ColumnValue">��ֵ����</param>  
        /// <param name="strWhereColumnName">��������</param>  
        /// <param name="strWhereColumnValue">������ֵ</param>  
        public static bool UpdateXmlRow(string strXmlPath, string[] Columns, string[] ColumnValue, string strWhereColumnName, string strWhereColumnValue)  
        {  
            try  
            {  
                string strXsdPath = strXmlPath.Substring(0, strXmlPath.IndexOf(".")) + ".xsd";  
                DataSet ds = new DataSet();  
                ds.ReadXmlSchema(GetXmlFullPath(strXsdPath));//��XML�ܹ�����ϵ���е���������  
                ds.ReadXml(GetXmlFullPath(strXmlPath));  
  
                //���ж�����  
                if (ds.Tables[0].Rows.Count > 0)  
                {  
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)  
                    {  
                        //�����ǰ��¼Ϊ����Where�����ļ�¼  
                        if (ds.Tables[0].Rows[i][strWhereColumnName].ToString().Trim().Equals(strWhereColumnValue))  
                        {  
                            //ѭ�����ҵ��еĸ��и���ֵ  
                            for (int j = 0; j < Columns.Length; j++)  
                            {  
                                ds.Tables[0].Rows[i][Columns[j]] = ColumnValue[j];  
                            }  
                            ds.AcceptChanges();                     //����DataSet  
                            ds.WriteXml(GetXmlFullPath(strXmlPath));//����д��XML�ļ�  
                            return true;  
                        }  
                    }  
  
                }  
                return false;  
            }  
            catch (Exception)  
            {  
                return false;  
            }  
        }  
        #endregion  
 
        #region ɾ������  
        /// <summary>  
        /// ɾ���ڵ�ֵ  
        /// </summary>  
        /// <param name="path">·��</param>  
        /// <param name="node">�ڵ�</param>  
        /// <param name="attribute">���������ǿ�ʱɾ���ýڵ�����ֵ������ɾ���ڵ�ֵ</param>  
        /// <param name="value">ֵ</param>  
        /// ʹ��ʾ��:  
        /// XMLProsess.Delete(path, "/Node", "")  
        /// XMLProsess.Delete(path, "/Node", "Attribute")  
        public static void Delete(string path, string node)  
        {  
            try  
            {  
                XmlDocument doc = XMLLoad(path);  
                XmlNode xn = doc.SelectSingleNode(node);  
                xn.ParentNode.RemoveChild(xn);  
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);  
            }  
            catch { }  
        }  
  
        /// <summary>  
        /// ɾ������  
        /// </summary>  
        /// <param name="path">·��</param>  
        /// <param name="node">�ڵ�</param>  
        /// <param name="attribute">���������ǿ�ʱɾ���ýڵ�����ֵ������ɾ���ڵ�ֵ</param>  
        /// <param name="value">ֵ</param>  
        /// ʹ��ʾ��:  
        /// XMLProsess.Delete(path, "/Node", "")  
        /// XMLProsess.Delete(path, "/Node", "Attribute")  
        public static void Delete(string path, string node, string attribute)  
        {  
            try  
            {  
                XmlDocument doc = XMLLoad(path);  
                XmlNode xn = doc.SelectSingleNode(node);  
                XmlElement xe = (XmlElement)xn;  
                if (attribute.Equals(""))  
                    xn.ParentNode.RemoveChild(xn);  
                else  
                    xe.RemoveAttribute(attribute);  
                doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);  
            }  
            catch { }  
        }  
  
        /// <summary>  
        /// ɾ��������  
        /// </summary>  
        /// <param name="strXmlPath">XML·��</param>  
        public static bool DeleteXmlAllRows(string strXmlPath)  
        {  
            try  
            {  
                DataSet ds = new DataSet();  
                ds.ReadXml(GetXmlFullPath(strXmlPath));  
                if (ds.Tables[0].Rows.Count > 0)  
                {  
                    ds.Tables[0].Rows.Clear();  
                }  
                ds.WriteXml(GetXmlFullPath(strXmlPath));  
                return true;  
            }  
            catch (Exception)  
            {  
                return false;  
            }  
        }  
  
        /// <summary>  
        /// ͨ��ɾ��DataSet��ָ�������У���дXML��ʵ��ɾ��ָ����  
        /// </summary>  
        /// <param name="iDeleteRow">Ҫɾ��������DataSet�е�Indexֵ</param>  
        public static bool DeleteXmlRowByIndex(string strXmlPath, int iDeleteRow)  
        {  
            try  
            {  
                DataSet ds = new DataSet();  
                ds.ReadXml(GetXmlFullPath(strXmlPath));  
                if (ds.Tables[0].Rows.Count > 0)  
                {  
                    ds.Tables[0].Rows[iDeleteRow].Delete();  
                }  
                ds.WriteXml(GetXmlFullPath(strXmlPath));  
                return true;  
            }  
            catch (Exception)  
            {  
                return false;  
            }  
        }  
  
        /// <summary>  
        /// ɾ��ָ������ָ��ֵ����  
        /// </summary>  
        /// <param name="strXmlPath">XML���·��</param>  
        /// <param name="strColumn">����</param>  
        /// <param name="ColumnValue">ָ��ֵ</param>  
        public static bool DeleteXmlRows(string strXmlPath, string strColumn, string[] ColumnValue)  
        {  
            try  
            {  
                DataSet ds = new DataSet();  
                ds.ReadXml(GetXmlFullPath(strXmlPath));  
                if (ds.Tables[0].Rows.Count > 0)  
                {  
                    //�ж��ж໹��ɾ����ֵ�࣬���forѭ����������  
                    if (ColumnValue.Length > ds.Tables[0].Rows.Count)  
                    {  
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)  
                        {  
                            for (int j = 0; j < ColumnValue.Length; j++)  
                            {  
                                if (ds.Tables[0].Rows[i][strColumn].ToString().Trim().Equals(ColumnValue[j]))  
                                {  
                                    ds.Tables[0].Rows[i].Delete();  
                                }  
                            }  
                        }  
                    }  
                    else  
                    {  
                        for (int j = 0; j < ColumnValue.Length; j++)  
                        {  
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)  
                            {  
                                if (ds.Tables[0].Rows[i][strColumn].ToString().Trim().Equals(ColumnValue[j]))  
                                {  
                                    ds.Tables[0].Rows[i].Delete();  
                                }  
                            }  
                        }  
                    }  
                    ds.WriteXml(GetXmlFullPath(strXmlPath));  
                }  
                return true;  
            }  
            catch (Exception)  
            {  
                return false;  
            }  
        }  
        #endregion 
        /// <summary>encoded/normal texts transfer to normal/encoded ones
        /// </summary>
        /// <param name="password">encoded/normal text</param>
        /// <returns></returns>
        private string EncodePassword(string password)
        {
            string encodedString = "";

            foreach (char signs in password)
            {
                char sign = (char)(signs ^ MAGICNUMBER);
                encodedString += sign.ToString();
            }

            return encodedString;
        }
    }  
}