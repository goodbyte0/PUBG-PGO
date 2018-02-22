using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace PGVisual.Overlay
{
	// Token: 0x02000019 RID: 25
	internal class HSClient
	{
		// Token: 0x0600023A RID: 570 RVA: 0x0000390D File Offset: 0x00001B0D
		public HSClient()
		{
			this._tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00003923 File Offset: 0x00001B23
		public bool SendInt(int value)
		{
			return this._tcpSocket.Send(BitConverter.GetBytes(value)) > 0;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00003939 File Offset: 0x00001B39
		public bool SendUInt(uint value)
		{
			return this._tcpSocket.Send(BitConverter.GetBytes(value)) > 0;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000394F File Offset: 0x00001B4F
		public bool SendUInt64(ulong value)
		{
			return this._tcpSocket.Send(BitConverter.GetBytes(value)) > 0;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00003965 File Offset: 0x00001B65
		public bool SendOperation(HSClient.HSProxyOperation operation)
		{
			return this.SendInt((int)operation);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000396E File Offset: 0x00001B6E
		public bool SendString(string str)
		{
			return this.SendInt(str.Length) && this._tcpSocket.Send(Encoding.ASCII.GetBytes(str)) > 0;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00008704 File Offset: 0x00006904
		public int ReadInt()
		{
			byte[] array = new byte[4];
			if (this._tcpSocket.Receive(array) <= 0)
			{
				throw new Exception("read failure");
			}
			return BitConverter.ToInt32(array, 0);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000873C File Offset: 0x0000693C
		public ulong ReadUInt64()
		{
			byte[] array = new byte[8];
			if (this._tcpSocket.Receive(array) <= 0)
			{
				throw new Exception("read failure");
			}
			return BitConverter.ToUInt64(array, 0);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000399A File Offset: 0x00001B9A
		public bool ReadBytes(byte[] buffer)
		{
			if (this._tcpSocket.Receive(buffer) <= 0)
			{
				throw new Exception("read failure");
			}
			return true;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00008774 File Offset: 0x00006974
		private static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
		{
			GCHandle gchandle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
			T result;
			try
			{
				result = (T)((object)Marshal.PtrToStructure(gchandle.AddrOfPinnedObject(), typeof(T)));
			}
			finally
			{
				gchandle.Free();
			}
			return result;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000087C0 File Offset: 0x000069C0
		public T ReadMemory<T>(ulong addr) where T : struct
		{
			byte[] array = new byte[Marshal.SizeOf(typeof(T))];
			if (!this.ReadMemory(addr, array))
			{
				throw new Exception("read memory failed");
			}
			return HSClient.ByteArrayToStructure<T>(array);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000039B7 File Offset: 0x00001BB7
		public ulong ReadPtr(ulong addr)
		{
			return this.ReadMemory<ulong>(addr);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000039C0 File Offset: 0x00001BC0
		public bool ReadMemory(ulong addr, byte[] buffer)
		{
			return this.SendOperation(HSClient.HSProxyOperation.READ_BUFFER) && this.SendUInt64(addr) && this.SendInt(buffer.Length) && this.ReadInt() == 1 && this.ReadInt() > 0 && this.ReadBytes(buffer);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000039FD File Offset: 0x00001BFD
		public ulong GetModuleBase(string module)
		{
			if (this.SendOperation(HSClient.HSProxyOperation.GET_MODULEBASE) && this.SendString(module))
			{
				return this.ReadUInt64();
			}
			return 0UL;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00008800 File Offset: 0x00006A00
		public bool Handshake(int processId, int version)
		{
			return this.SendUInt(3735928559u) && this.SendInt(version) && this.SendInt(processId) && this.ReadInt() == 1;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00008838 File Offset: 0x00006A38
		public bool connect()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\HSProxy"))
			{
				if (registryKey != null)
				{
					int port = Convert.ToInt32(registryKey.GetValue("Port"));
					try
					{
						this._tcpSocket.Connect("127.0.0.1", port);
					}
					catch (Exception)
					{
						return false;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000054 RID: 84
		private Socket _tcpSocket;

		// Token: 0x0200001A RID: 26
		public enum HSProxyOperation
		{
			// Token: 0x04000056 RID: 86
			WRITE_BUFFER,
			// Token: 0x04000057 RID: 87
			READ_BUFFER,
			// Token: 0x04000058 RID: 88
			CREATE_THREAD,
			// Token: 0x04000059 RID: 89
			ALLOCATE,
			// Token: 0x0400005A RID: 90
			GET_MODULEBASE
		}
	}
}
