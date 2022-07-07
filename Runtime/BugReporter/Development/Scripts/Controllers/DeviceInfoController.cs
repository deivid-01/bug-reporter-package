using System.Collections.Generic;

using UnityEngine;


public class DeviceInfoController : MonoBehaviour
{
    
     private List<string> systemInformation;
     
    private void Awake()
    {
        SetSystemInformation();
    }
    
    private void SetSystemInformation()
    {
        
        systemInformation = new List<string>(); 
        
        AddDeviceSpecification("Resolution",$"{Screen.currentResolution.width}x{Screen.currentResolution.height}");
        AddDeviceSpecification("Operating System",SystemInfo.operatingSystem);
        AddDeviceSpecification("Platform", Application.platform.ToString());
        AddDeviceSpecification("Game running full-screen", Screen.fullScreen.ToString());
        AddDeviceSpecification("Device Model", SystemInfo.deviceModel);
        AddDeviceSpecification("Device Type", SystemInfo.deviceType.ToString());
        AddDeviceSpecification("Graphics Device Name", SystemInfo.graphicsDeviceName);
        AddDeviceSpecification("Graphics Memory Size", SystemInfo.graphicsMemorySize.ToString());
        AddDeviceSpecification("Processor Type",SystemInfo.processorType);
        AddDeviceSpecification("Processor Count",SystemInfo.processorCount.ToString());
    }
    
    private void AddDeviceSpecification(string title, string description)
    {
        systemInformation.Add($"{title}: {description}");
    }

    public string[] SystemInformation => systemInformation.ToArray();




}
