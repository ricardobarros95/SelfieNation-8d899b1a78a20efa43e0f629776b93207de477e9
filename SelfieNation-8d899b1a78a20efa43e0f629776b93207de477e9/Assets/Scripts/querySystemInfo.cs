using UnityEngine;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

public class querySystemInfo : MonoBehaviour {

    string sysInfo;

	// Use this for initialization
	void Start () {

       sysInfo = "deviceModel="+SystemInfo.deviceModel+"|*|";
       sysInfo += "deviceName="+SystemInfo.deviceName+"|*|";
       sysInfo += "deviceType="+SystemInfo.deviceType+"|*|";
       sysInfo += "deviceUniqueIdentifier="+SystemInfo.deviceUniqueIdentifier+"|*|";
       sysInfo += "graphicsDeviceID="+SystemInfo.graphicsDeviceID+"|*|";
       sysInfo += "graphicsDeviceName="+SystemInfo.graphicsDeviceName+"|*|";
       sysInfo += "graphicsDeviceType="+SystemInfo.graphicsDeviceType+"|*|";
       sysInfo += "graphicsDeviceVendor="+SystemInfo.graphicsDeviceVendor+"|*|";
       sysInfo += "graphicsDeviceVendorID="+SystemInfo.graphicsDeviceVendorID+"|*|";
       sysInfo += "graphicsDeviceVersion="+SystemInfo.graphicsDeviceVersion+"|*|";
       sysInfo += "graphicsMemorySize="+SystemInfo.graphicsMemorySize+"|*|";
       sysInfo += "graphicsMultiThreaded="+SystemInfo.graphicsMultiThreaded+"|*|";
       sysInfo += "graphicsShaderLevel="+SystemInfo.graphicsShaderLevel+"|*|";
       sysInfo += "npotSupport="+SystemInfo.npotSupport+"|*|";
       sysInfo += "operatingSystem="+SystemInfo.operatingSystem+"|*|";
       sysInfo += "processorCount="+SystemInfo.processorCount+"|*|";
       sysInfo += "processorType="+SystemInfo.processorType+"|*|";
       sysInfo += "supportedRenderTargetCount="+SystemInfo.supportedRenderTargetCount+"|*|";
       sysInfo += "supports3DTextures="+SystemInfo.supports3DTextures+"|*|";
       sysInfo += "supportsAccelerometer="+SystemInfo.supportsAccelerometer+"|*|";
       sysInfo += "supportsComputeShaders="+SystemInfo.supportsComputeShaders+"|*|";
       sysInfo += "supportsGyroscope="+SystemInfo.supportsGyroscope+"|*|";
       sysInfo += "supportsImageEffects="+SystemInfo.supportsImageEffects+"|*|";
       sysInfo += "supportsInstancing="+SystemInfo.supportsInstancing+"|*|";
       sysInfo += "supportsLocationService="+SystemInfo.supportsLocationService+"|*|";
       sysInfo += "supportsRenderTextures="+SystemInfo.supportsRenderTextures+"|*|";
       sysInfo += "supportsRenderToCubemap="+SystemInfo.supportsRenderToCubemap+"|*|";
       sysInfo += "supportsShadows="+SystemInfo.supportsShadows+"|*|";
       sysInfo += "supportsSparseTextures="+SystemInfo.supportsSparseTextures+"|*|";
       sysInfo += "supportsStencil="+SystemInfo.supportsStencil+"|*|";
       sysInfo += "supportsVibration="+SystemInfo.supportsVibration+"|*|";
       sysInfo += "systemMemorySize=" + SystemInfo.systemMemorySize + "|*|";
          



        Debug.Log("-----------------------------> " + sysInfo);

        System.IO.File.WriteAllText("d:/tmp/SysinfoZ.txt", sysInfo);

        string filePath = Application.persistentDataPath + "/SysinfoZ.txt";
        
        System.IO.File.WriteAllText(filePath, sysInfo);


        byte[] infoInBytes = Encoding.ASCII.GetBytes(sysInfo);
        File.WriteAllBytes(Application.persistentDataPath + "/SysinfoZ.txt", infoInBytes);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
