﻿using System;

namespace CodeVault_DotNet
{
    class Program
    {
        static void Main(string[] args)
        {

            //ImageCompressor imageCompressor = new ImageCompressor();
            //imageCompressor.CompressImage();

            //EncryptingTool encryptingTool = new EncryptingTool();
            ////encryptingTool.EncryptString();
            //encryptingTool.DecryptString();

            EmailDemo sendGridDemo = new EmailDemo();
            sendGridDemo.SendEmail().Wait();
        }
    }
}
