using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PopulateTreeView();
        }

        private void PopulateTreeView()
        {
            DigitalCamera camera1 = new DigitalCamera();
            DigitalCamera camera2 = new DigitalCamera("Canon", 20.1, true, new List<string> { "Auto", "Portrait", "Night" });

            ShowCameraInfo(camera1, "�ᒺ�� 1");
            ShowCameraInfo(camera2, "�ᒺ�� 2");
        }

        private void ShowCameraInfo(DigitalCamera camera, string nodeTitle)
        {
            Type type = typeof(DigitalCamera);

            TreeNode rootNode = new TreeNode(nodeTitle); // ����� ��'����

            // ����������
            TreeNode propertiesNode = new TreeNode("����������");
            foreach (PropertyInfo prop in type.GetProperties())
            {
                object value = prop.GetValue(camera);
                string displayValue = value is List<string> list ? string.Join(", ", list) : value?.ToString();
                propertiesNode.Nodes.Add($"{prop.PropertyType.Name} {prop.Name} = {displayValue}");
            }
            rootNode.Nodes.Add(propertiesNode);

            // ������
            TreeNode methodsNode = new TreeNode("������");

            // �������� ������ �� ����������
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (MethodInfo method in methods)
            {
                string methodSignature = method.ReturnType.Name + " " + method.Name + "(";

                ParameterInfo[] parameters = method.GetParameters();
                foreach (ParameterInfo param in parameters)
                {
                    methodSignature += param.ParameterType.Name + " " + param.Name + ", ";
                }

                if (parameters.Length > 0)
                    methodSignature = methodSignature.Remove(methodSignature.Length - 2);

                methodSignature += ")";
                methodsNode.Nodes.Add(methodSignature);
            }
            rootNode.Nodes.Add(methodsNode);

            treeView1.Nodes.Add(rootNode);
            treeView1.ExpandAll();

            // ������ ������ ����� �������� 
            MethodInfo m = type.GetMethod("GetCameraInfo");
            if (m != null && m.GetParameters().Length == 0)
            {
                object result = m.Invoke(camera, null);
                if (result != null)
                    MessageBox.Show($"[{nodeTitle}]\n" + result.ToString());
            }
        }
    }

    public class DigitalCamera
    {
        public string Brand { get; set; }
        public double Megapixels { get; set; }
        public bool HasWiFi { get; set; }
        public List<string> ShootingModes { get; set; }

        public DigitalCamera()
        {
            Brand = "Unknown";
            Megapixels = 12.0;
            HasWiFi = false;
            ShootingModes = new List<string> { "Auto", "Manual" };
        }

        public DigitalCamera(string brand, double megapixels, bool hasWiFi, List<string> modes)
        {
            Brand = brand;
            Megapixels = megapixels;
            HasWiFi = hasWiFi;
            ShootingModes = modes;
        }

        public void TakePhoto()
        {
            MessageBox.Show($"{Brand} ������ ���� � ��������� �������� {Megapixels} ��.");
        }

        public void ConnectToWiFi()
        {
            MessageBox.Show(HasWiFi ? $"{Brand} ��������� �� Wi-Fi." : $"{Brand} �� ������� Wi-Fi.");
        }

        public string GetCameraInfo()
        {
            return $"{Brand} - {Megapixels} ��, Wi-Fi: {(HasWiFi ? "�" : "����")}";
        }
    }


}
