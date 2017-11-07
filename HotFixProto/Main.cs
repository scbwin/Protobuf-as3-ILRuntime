﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HotFixProto
{
    public class Main
    {
		public void Initialize()
		{
			Test1();
			Test2();
			Test3();
			Test4();
			Test5();
		}

		private static  void Test1()
		{
			UnityEngine.Debug.Log("repeat 嵌套对象 测试");

			SProtoSpace.area_list_info_ack role = new SProtoSpace.area_list_info_ack();
			role.account = "你大爷的";
			role.areaGroups.Add(new SProtoSpace.group_area_info() { areaGroupName = "组1", groupids = new List<uint>(new uint[] { 1, 2, 3, 4 }) });
			role.areaGroups.Add(new SProtoSpace.group_area_info() { areaGroupName = "组2", groupids = new List<uint>(new uint[] { 5, 6, 7, 8 }) });

			role.myLoginAreas = new SProtoSpace.group_area_info();
			role.myLoginAreas.areaGroupName = "JJKKN";
			

			role.recommendAreas.Add(role.areaGroups[0]);

			UnityEngine.Debug.Log(role.account);

			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			role.writeTo(ms);

			byte[] bytes = ms.ToArray();

			UnityEngine.Debug.Log(role.printByte(bytes));

			ms.Dispose();

			SProtoSpace.area_list_info_ack role2 = new SProtoSpace.area_list_info_ack();
			role2.mergeFrom(new System.IO.MemoryStream(bytes));
			UnityEngine.Debug.Log("反序列化:" + role2.account);

			UnityEngine.Debug.Log("反序列化:" + role2.myLoginAreas);
			UnityEngine.Debug.Log("反序列化:" + role2.myLoginAreas.areaGroupName);
			UnityEngine.Debug.Log("反序列化:" + role2.myLoginAreas.groupids);

			for (int i = 0; i < role2.areaGroups.Count; i++)
			{
				var g = role2.areaGroups[i];
				UnityEngine.Debug.Log("反序列化:" + g);
				UnityEngine.Debug.Log("反序列化: areaGroupName" + g.areaGroupName);
				for (int j = 0; j < g.groupids.Count; j++)
				{
					UnityEngine.Debug.Log("反序列化: groupids:" + g.groupids[j]);
				}

			}
			UnityEngine.Debug.Log("反序列化: recommendAreas.Count:" + role2.recommendAreas.Count);
			UnityEngine.Debug.Log("反序列化: recommendAreas[0].areaGroupName" + role2.recommendAreas[0].areaGroupName);
			for (int i = 0; i < role2.recommendAreas[0].groupids.Count; i++)
			{
				UnityEngine.Debug.Log("反序列化: groupids:" + role2.recommendAreas[0].groupids[i]);
			}



			UnityEngine.Debug.Log("======");
		}

		private static void Test2()
		{
			UnityEngine.Debug.Log("byte[] 序列化测试");

			SProtoSpace.common_protocol_forwarded_ack role = new SProtoSpace.common_protocol_forwarded_ack();
			role.packetData = new byte[] { 4, 5, 6, 7, 8 };

			UnityEngine.Debug.Log(role.packetData);

			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			role.writeTo(ms);

			byte[] bytes = ms.ToArray();

			UnityEngine.Debug.Log(role.printByte(bytes));

			ms.Dispose();

			SProtoSpace.common_protocol_forwarded_ack role2 = new SProtoSpace.common_protocol_forwarded_ack();
			role2.mergeFrom(new System.IO.MemoryStream(bytes));
			UnityEngine.Debug.Log("反序列化:" + role2.packetData);
			for (int i = 0; i < role2.packetData.Length; i++)
			{
				UnityEngine.Debug.Log("反序列化:" + role2.packetData[i]);
			}

			UnityEngine.Debug.Log("======");
		}

		private static void Test3()
		{
			UnityEngine.Debug.Log("optional 序列化测试");

			//message login_req
			//{
			//	required string account = 1;
			//	required string password = 2;
			//	optional bool isdetail = 3;
			//	optional string userid = 4;
			//	optional string usersession = 5;
			//	optional EAuthType loginType = 6;
			//}

			SProtoSpace.login_req role = new SProtoSpace.login_req();
			UnityEngine.Debug.Log(role.account);
			role.password = "PASSWORD";
			UnityEngine.Debug.Log(role.password);
			role.isdetail = true;
			UnityEngine.Debug.Log(role.hasIsdetail);
			

			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			role.writeTo(ms);

			byte[] bytes = ms.ToArray();

			UnityEngine.Debug.Log(role.printByte(bytes));

			ms.Dispose();

			SProtoSpace.login_req role2 = new SProtoSpace.login_req();
			role2.mergeFrom(new System.IO.MemoryStream(bytes));
			UnityEngine.Debug.Log("反序列化:" + role2.hasIsdetail);
			UnityEngine.Debug.Log("反序列化:" + role2.password);


			UnityEngine.Debug.Log("======");
		}

		private static void Test4()
		{
			UnityEngine.Debug.Log("枚举 序列化测试");

			
			SProtoSpace.login_req role = new SProtoSpace.login_req();
			UnityEngine.Debug.Log(role.account);
			role.password = "PASSWORD";
			UnityEngine.Debug.Log(role.password);
			UnityEngine.Debug.Log(role.testEnum);

			role.testEnum = SProtoSpace.protoId.login_need_queued_ntf_id;

			role.isdetail = true;
			role.loginType = SProtoSpace.EAuthType.AUTH_XIAOMI;
			UnityEngine.Debug.Log(role.hasIsdetail);


			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			role.writeTo(ms);

			byte[] bytes = ms.ToArray();

			UnityEngine.Debug.Log(role.printByte(bytes));

			ms.Dispose();

			SProtoSpace.login_req role2 = new SProtoSpace.login_req();
			role2.mergeFrom(new System.IO.MemoryStream(bytes));
			UnityEngine.Debug.Log("反序列化:" + role2.hasIsdetail);
			UnityEngine.Debug.Log("反序列化:" + role2.password);
			UnityEngine.Debug.Log("反序列化:" + role2.loginType);
			UnityEngine.Debug.Log("反序列化:" + role2.testEnum);

			UnityEngine.Debug.Log("======");
		}

		private static void Test5()
		{
			UnityEngine.Debug.Log("repeat 枚举 序列化测试");


			SProtoSpace.login_ack role = new SProtoSpace.login_ack();
			UnityEngine.Debug.Log(role.ret);
			
			UnityEngine.Debug.Log(role.authtypelist);
			role.authtypelist.Add(SProtoSpace.EAuthType.AUTH_SELF);
			role.authtypelist.Add(SProtoSpace.EAuthType.AUTH_XIAOMI);

			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			role.writeTo(ms);

			byte[] bytes = ms.ToArray();

			UnityEngine.Debug.Log(role.printByte(bytes));
			ms.Dispose();

			SProtoSpace.login_ack role2 = new SProtoSpace.login_ack();
			role2.mergeFrom(new System.IO.MemoryStream(bytes));
			UnityEngine.Debug.Log("反序列化:" + role2.ret);
			UnityEngine.Debug.Log("反序列化:" + role2.authtypelist[0]);
			UnityEngine.Debug.Log("反序列化:" + role2.authtypelist[1]);
			

			UnityEngine.Debug.Log("======");
		}

		public static void Update()
		{
			
		}

    }
}
