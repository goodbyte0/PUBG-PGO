using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace PGVisual.Overlay
{
	// Token: 0x0200001B RID: 27
	public class Native
	{
		// Token: 0x0600024A RID: 586
		[DllImport("ntdll.dll")]
		public static extern Native.NTSTATUS RtlInitUnicodeString(ref Native.UNICODE_STRING DestinationString, [MarshalAs(UnmanagedType.LPWStr)] string SourceString);

		// Token: 0x0600024B RID: 587
		[DllImport("ntdll.dll", SetLastError = true)]
		public static extern Native.NTSTATUS RtlAdjustPrivilege(int Privilege, bool Enable, Native.ADJUST_PRIVILEGE_TYPE CurrentThread, out bool Enabled);

		// Token: 0x0600024C RID: 588
		[DllImport("ntdll.dll", SetLastError = true)]
		public static extern Native.NTSTATUS NtLoadDriver(ref Native.UNICODE_STRING DriverServiceName);

		// Token: 0x0600024D RID: 589
		[DllImport("ntdll.dll", SetLastError = true)]
		public static extern Native.NTSTATUS NtUnloadDriver(ref Native.UNICODE_STRING DriverServiceName);

		// Token: 0x0600024E RID: 590
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern SafeFileHandle CreateFile(string fileName, [MarshalAs(UnmanagedType.U4)] FileAccess fileAccess, [MarshalAs(UnmanagedType.U4)] FileShare fileShare, IntPtr securityAttributes, [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition, [MarshalAs(UnmanagedType.U4)] FileAttributes flagsAndAttributes, IntPtr template);

		// Token: 0x0600024F RID: 591
		[DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool DeviceIoControl(SafeFileHandle hDevice, uint IoControlCode, [MarshalAs(UnmanagedType.AsAny)] [In] object InBuffer, uint nInBufferSize, [MarshalAs(UnmanagedType.AsAny)] [Out] object OutBuffer, uint nOutBufferSize, ref int pBytesReturned, IntPtr Overlapped);

		// Token: 0x06000250 RID: 592 RVA: 0x00003A21 File Offset: 0x00001C21
		public static uint CTL_CODE(uint DeviceType, uint Function, uint Method, uint Access)
		{
			return DeviceType << 16 | Access << 14 | Function << 2 | Method;
		}

		// Token: 0x0400005B RID: 91
		public const int SeLoadDriverPrivilege = 10;

		// Token: 0x0400005C RID: 92
		public const int SeDebugPrivilege = 20;

		// Token: 0x0200001C RID: 28
		public enum NTSTATUS : uint
		{
			// Token: 0x0400005E RID: 94
			Success,
			// Token: 0x0400005F RID: 95
			Wait0 = 0u,
			// Token: 0x04000060 RID: 96
			Wait1,
			// Token: 0x04000061 RID: 97
			Wait2,
			// Token: 0x04000062 RID: 98
			Wait3,
			// Token: 0x04000063 RID: 99
			Wait63 = 63u,
			// Token: 0x04000064 RID: 100
			Abandoned = 128u,
			// Token: 0x04000065 RID: 101
			AbandonedWait0 = 128u,
			// Token: 0x04000066 RID: 102
			AbandonedWait1,
			// Token: 0x04000067 RID: 103
			AbandonedWait2,
			// Token: 0x04000068 RID: 104
			AbandonedWait3,
			// Token: 0x04000069 RID: 105
			AbandonedWait63 = 191u,
			// Token: 0x0400006A RID: 106
			UserApc,
			// Token: 0x0400006B RID: 107
			KernelApc = 256u,
			// Token: 0x0400006C RID: 108
			Alerted,
			// Token: 0x0400006D RID: 109
			Timeout,
			// Token: 0x0400006E RID: 110
			Pending,
			// Token: 0x0400006F RID: 111
			Reparse,
			// Token: 0x04000070 RID: 112
			MoreEntries,
			// Token: 0x04000071 RID: 113
			NotAllAssigned,
			// Token: 0x04000072 RID: 114
			SomeNotMapped,
			// Token: 0x04000073 RID: 115
			OpLockBreakInProgress,
			// Token: 0x04000074 RID: 116
			VolumeMounted,
			// Token: 0x04000075 RID: 117
			RxActCommitted,
			// Token: 0x04000076 RID: 118
			NotifyCleanup,
			// Token: 0x04000077 RID: 119
			NotifyEnumDir,
			// Token: 0x04000078 RID: 120
			NoQuotasForAccount,
			// Token: 0x04000079 RID: 121
			PrimaryTransportConnectFailed,
			// Token: 0x0400007A RID: 122
			PageFaultTransition = 272u,
			// Token: 0x0400007B RID: 123
			PageFaultDemandZero,
			// Token: 0x0400007C RID: 124
			PageFaultCopyOnWrite,
			// Token: 0x0400007D RID: 125
			PageFaultGuardPage,
			// Token: 0x0400007E RID: 126
			PageFaultPagingFile,
			// Token: 0x0400007F RID: 127
			CrashDump = 278u,
			// Token: 0x04000080 RID: 128
			ReparseObject = 280u,
			// Token: 0x04000081 RID: 129
			NothingToTerminate = 290u,
			// Token: 0x04000082 RID: 130
			ProcessNotInJob,
			// Token: 0x04000083 RID: 131
			ProcessInJob,
			// Token: 0x04000084 RID: 132
			ProcessCloned = 297u,
			// Token: 0x04000085 RID: 133
			FileLockedWithOnlyReaders,
			// Token: 0x04000086 RID: 134
			FileLockedWithWriters,
			// Token: 0x04000087 RID: 135
			Informational = 1073741824u,
			// Token: 0x04000088 RID: 136
			ObjectNameExists = 1073741824u,
			// Token: 0x04000089 RID: 137
			ThreadWasSuspended,
			// Token: 0x0400008A RID: 138
			WorkingSetLimitRange,
			// Token: 0x0400008B RID: 139
			ImageNotAtBase,
			// Token: 0x0400008C RID: 140
			RegistryRecovered = 1073741833u,
			// Token: 0x0400008D RID: 141
			Warning = 2147483648u,
			// Token: 0x0400008E RID: 142
			GuardPageViolation,
			// Token: 0x0400008F RID: 143
			DatatypeMisalignment,
			// Token: 0x04000090 RID: 144
			Breakpoint,
			// Token: 0x04000091 RID: 145
			SingleStep,
			// Token: 0x04000092 RID: 146
			BufferOverflow,
			// Token: 0x04000093 RID: 147
			NoMoreFiles,
			// Token: 0x04000094 RID: 148
			HandlesClosed = 2147483658u,
			// Token: 0x04000095 RID: 149
			PartialCopy = 2147483661u,
			// Token: 0x04000096 RID: 150
			DeviceBusy = 2147483665u,
			// Token: 0x04000097 RID: 151
			InvalidEaName = 2147483667u,
			// Token: 0x04000098 RID: 152
			EaListInconsistent,
			// Token: 0x04000099 RID: 153
			NoMoreEntries = 2147483674u,
			// Token: 0x0400009A RID: 154
			LongJump = 2147483686u,
			// Token: 0x0400009B RID: 155
			DllMightBeInsecure = 2147483691u,
			// Token: 0x0400009C RID: 156
			Error = 3221225472u,
			// Token: 0x0400009D RID: 157
			Unsuccessful,
			// Token: 0x0400009E RID: 158
			NotImplemented,
			// Token: 0x0400009F RID: 159
			InvalidInfoClass,
			// Token: 0x040000A0 RID: 160
			InfoLengthMismatch,
			// Token: 0x040000A1 RID: 161
			AccessViolation,
			// Token: 0x040000A2 RID: 162
			InPageError,
			// Token: 0x040000A3 RID: 163
			PagefileQuota,
			// Token: 0x040000A4 RID: 164
			InvalidHandle,
			// Token: 0x040000A5 RID: 165
			BadInitialStack,
			// Token: 0x040000A6 RID: 166
			BadInitialPc,
			// Token: 0x040000A7 RID: 167
			InvalidCid,
			// Token: 0x040000A8 RID: 168
			TimerNotCanceled,
			// Token: 0x040000A9 RID: 169
			InvalidParameter,
			// Token: 0x040000AA RID: 170
			NoSuchDevice,
			// Token: 0x040000AB RID: 171
			NoSuchFile,
			// Token: 0x040000AC RID: 172
			InvalidDeviceRequest,
			// Token: 0x040000AD RID: 173
			EndOfFile,
			// Token: 0x040000AE RID: 174
			WrongVolume,
			// Token: 0x040000AF RID: 175
			NoMediaInDevice,
			// Token: 0x040000B0 RID: 176
			NoMemory = 3221225495u,
			// Token: 0x040000B1 RID: 177
			NotMappedView = 3221225497u,
			// Token: 0x040000B2 RID: 178
			UnableToFreeVm,
			// Token: 0x040000B3 RID: 179
			UnableToDeleteSection,
			// Token: 0x040000B4 RID: 180
			IllegalInstruction = 3221225501u,
			// Token: 0x040000B5 RID: 181
			AlreadyCommitted = 3221225505u,
			// Token: 0x040000B6 RID: 182
			AccessDenied,
			// Token: 0x040000B7 RID: 183
			BufferTooSmall,
			// Token: 0x040000B8 RID: 184
			ObjectTypeMismatch,
			// Token: 0x040000B9 RID: 185
			NonContinuableException,
			// Token: 0x040000BA RID: 186
			BadStack = 3221225512u,
			// Token: 0x040000BB RID: 187
			NotLocked = 3221225514u,
			// Token: 0x040000BC RID: 188
			NotCommitted = 3221225517u,
			// Token: 0x040000BD RID: 189
			InvalidParameterMix = 3221225520u,
			// Token: 0x040000BE RID: 190
			ObjectNameInvalid = 3221225523u,
			// Token: 0x040000BF RID: 191
			ObjectNameNotFound,
			// Token: 0x040000C0 RID: 192
			ObjectNameCollision,
			// Token: 0x040000C1 RID: 193
			ObjectPathInvalid = 3221225529u,
			// Token: 0x040000C2 RID: 194
			ObjectPathNotFound,
			// Token: 0x040000C3 RID: 195
			ObjectPathSyntaxBad,
			// Token: 0x040000C4 RID: 196
			DataOverrun,
			// Token: 0x040000C5 RID: 197
			DataLate,
			// Token: 0x040000C6 RID: 198
			DataError,
			// Token: 0x040000C7 RID: 199
			CrcError,
			// Token: 0x040000C8 RID: 200
			SectionTooBig,
			// Token: 0x040000C9 RID: 201
			PortConnectionRefused,
			// Token: 0x040000CA RID: 202
			InvalidPortHandle,
			// Token: 0x040000CB RID: 203
			SharingViolation,
			// Token: 0x040000CC RID: 204
			QuotaExceeded,
			// Token: 0x040000CD RID: 205
			InvalidPageProtection,
			// Token: 0x040000CE RID: 206
			MutantNotOwned,
			// Token: 0x040000CF RID: 207
			SemaphoreLimitExceeded,
			// Token: 0x040000D0 RID: 208
			PortAlreadySet,
			// Token: 0x040000D1 RID: 209
			SectionNotImage,
			// Token: 0x040000D2 RID: 210
			SuspendCountExceeded,
			// Token: 0x040000D3 RID: 211
			ThreadIsTerminating,
			// Token: 0x040000D4 RID: 212
			BadWorkingSetLimit,
			// Token: 0x040000D5 RID: 213
			IncompatibleFileMap,
			// Token: 0x040000D6 RID: 214
			SectionProtection,
			// Token: 0x040000D7 RID: 215
			EasNotSupported,
			// Token: 0x040000D8 RID: 216
			EaTooLarge,
			// Token: 0x040000D9 RID: 217
			NonExistentEaEntry,
			// Token: 0x040000DA RID: 218
			NoEasOnFile,
			// Token: 0x040000DB RID: 219
			EaCorruptError,
			// Token: 0x040000DC RID: 220
			FileLockConflict,
			// Token: 0x040000DD RID: 221
			LockNotGranted,
			// Token: 0x040000DE RID: 222
			DeletePending,
			// Token: 0x040000DF RID: 223
			CtlFileNotSupported,
			// Token: 0x040000E0 RID: 224
			UnknownRevision,
			// Token: 0x040000E1 RID: 225
			RevisionMismatch,
			// Token: 0x040000E2 RID: 226
			InvalidOwner,
			// Token: 0x040000E3 RID: 227
			InvalidPrimaryGroup,
			// Token: 0x040000E4 RID: 228
			NoImpersonationToken,
			// Token: 0x040000E5 RID: 229
			CantDisableMandatory,
			// Token: 0x040000E6 RID: 230
			NoLogonServers,
			// Token: 0x040000E7 RID: 231
			NoSuchLogonSession,
			// Token: 0x040000E8 RID: 232
			NoSuchPrivilege,
			// Token: 0x040000E9 RID: 233
			PrivilegeNotHeld,
			// Token: 0x040000EA RID: 234
			InvalidAccountName,
			// Token: 0x040000EB RID: 235
			UserExists,
			// Token: 0x040000EC RID: 236
			NoSuchUser,
			// Token: 0x040000ED RID: 237
			GroupExists,
			// Token: 0x040000EE RID: 238
			NoSuchGroup,
			// Token: 0x040000EF RID: 239
			MemberInGroup,
			// Token: 0x040000F0 RID: 240
			MemberNotInGroup,
			// Token: 0x040000F1 RID: 241
			LastAdmin,
			// Token: 0x040000F2 RID: 242
			WrongPassword,
			// Token: 0x040000F3 RID: 243
			IllFormedPassword,
			// Token: 0x040000F4 RID: 244
			PasswordRestriction,
			// Token: 0x040000F5 RID: 245
			LogonFailure,
			// Token: 0x040000F6 RID: 246
			AccountRestriction,
			// Token: 0x040000F7 RID: 247
			InvalidLogonHours,
			// Token: 0x040000F8 RID: 248
			InvalidWorkstation,
			// Token: 0x040000F9 RID: 249
			PasswordExpired,
			// Token: 0x040000FA RID: 250
			AccountDisabled,
			// Token: 0x040000FB RID: 251
			NoneMapped,
			// Token: 0x040000FC RID: 252
			TooManyLuidsRequested,
			// Token: 0x040000FD RID: 253
			LuidsExhausted,
			// Token: 0x040000FE RID: 254
			InvalidSubAuthority,
			// Token: 0x040000FF RID: 255
			InvalidAcl,
			// Token: 0x04000100 RID: 256
			InvalidSid,
			// Token: 0x04000101 RID: 257
			InvalidSecurityDescr,
			// Token: 0x04000102 RID: 258
			ProcedureNotFound,
			// Token: 0x04000103 RID: 259
			InvalidImageFormat,
			// Token: 0x04000104 RID: 260
			NoToken,
			// Token: 0x04000105 RID: 261
			BadInheritanceAcl,
			// Token: 0x04000106 RID: 262
			RangeNotLocked,
			// Token: 0x04000107 RID: 263
			DiskFull,
			// Token: 0x04000108 RID: 264
			ServerDisabled,
			// Token: 0x04000109 RID: 265
			ServerNotDisabled,
			// Token: 0x0400010A RID: 266
			TooManyGuidsRequested,
			// Token: 0x0400010B RID: 267
			GuidsExhausted,
			// Token: 0x0400010C RID: 268
			InvalidIdAuthority,
			// Token: 0x0400010D RID: 269
			AgentsExhausted,
			// Token: 0x0400010E RID: 270
			InvalidVolumeLabel,
			// Token: 0x0400010F RID: 271
			SectionNotExtended,
			// Token: 0x04000110 RID: 272
			NotMappedData,
			// Token: 0x04000111 RID: 273
			ResourceDataNotFound,
			// Token: 0x04000112 RID: 274
			ResourceTypeNotFound,
			// Token: 0x04000113 RID: 275
			ResourceNameNotFound,
			// Token: 0x04000114 RID: 276
			ArrayBoundsExceeded,
			// Token: 0x04000115 RID: 277
			FloatDenormalOperand,
			// Token: 0x04000116 RID: 278
			FloatDivideByZero,
			// Token: 0x04000117 RID: 279
			FloatInexactResult,
			// Token: 0x04000118 RID: 280
			FloatInvalidOperation,
			// Token: 0x04000119 RID: 281
			FloatOverflow,
			// Token: 0x0400011A RID: 282
			FloatStackCheck,
			// Token: 0x0400011B RID: 283
			FloatUnderflow,
			// Token: 0x0400011C RID: 284
			IntegerDivideByZero,
			// Token: 0x0400011D RID: 285
			IntegerOverflow,
			// Token: 0x0400011E RID: 286
			PrivilegedInstruction,
			// Token: 0x0400011F RID: 287
			TooManyPagingFiles,
			// Token: 0x04000120 RID: 288
			FileInvalid,
			// Token: 0x04000121 RID: 289
			InstanceNotAvailable = 3221225643u,
			// Token: 0x04000122 RID: 290
			PipeNotAvailable,
			// Token: 0x04000123 RID: 291
			InvalidPipeState,
			// Token: 0x04000124 RID: 292
			PipeBusy,
			// Token: 0x04000125 RID: 293
			IllegalFunction,
			// Token: 0x04000126 RID: 294
			PipeDisconnected,
			// Token: 0x04000127 RID: 295
			PipeClosing,
			// Token: 0x04000128 RID: 296
			PipeConnected,
			// Token: 0x04000129 RID: 297
			PipeListening,
			// Token: 0x0400012A RID: 298
			InvalidReadMode,
			// Token: 0x0400012B RID: 299
			IoTimeout,
			// Token: 0x0400012C RID: 300
			FileForcedClosed,
			// Token: 0x0400012D RID: 301
			ProfilingNotStarted,
			// Token: 0x0400012E RID: 302
			ProfilingNotStopped,
			// Token: 0x0400012F RID: 303
			DeviceDoesNotExist = 3221225664u,
			// Token: 0x04000130 RID: 304
			NotSameDevice = 3221225684u,
			// Token: 0x04000131 RID: 305
			FileRenamed,
			// Token: 0x04000132 RID: 306
			CantWait = 3221225688u,
			// Token: 0x04000133 RID: 307
			PipeEmpty,
			// Token: 0x04000134 RID: 308
			CantTerminateSelf = 3221225691u,
			// Token: 0x04000135 RID: 309
			InternalError = 3221225701u,
			// Token: 0x04000136 RID: 310
			InvalidParameter1 = 3221225711u,
			// Token: 0x04000137 RID: 311
			InvalidParameter2,
			// Token: 0x04000138 RID: 312
			InvalidParameter3,
			// Token: 0x04000139 RID: 313
			InvalidParameter4,
			// Token: 0x0400013A RID: 314
			InvalidParameter5,
			// Token: 0x0400013B RID: 315
			InvalidParameter6,
			// Token: 0x0400013C RID: 316
			InvalidParameter7,
			// Token: 0x0400013D RID: 317
			InvalidParameter8,
			// Token: 0x0400013E RID: 318
			InvalidParameter9,
			// Token: 0x0400013F RID: 319
			InvalidParameter10,
			// Token: 0x04000140 RID: 320
			InvalidParameter11,
			// Token: 0x04000141 RID: 321
			InvalidParameter12,
			// Token: 0x04000142 RID: 322
			MappedFileSizeZero = 3221225758u,
			// Token: 0x04000143 RID: 323
			TooManyOpenedFiles,
			// Token: 0x04000144 RID: 324
			Cancelled,
			// Token: 0x04000145 RID: 325
			CannotDelete,
			// Token: 0x04000146 RID: 326
			InvalidComputerName,
			// Token: 0x04000147 RID: 327
			FileDeleted,
			// Token: 0x04000148 RID: 328
			SpecialAccount,
			// Token: 0x04000149 RID: 329
			SpecialGroup,
			// Token: 0x0400014A RID: 330
			SpecialUser,
			// Token: 0x0400014B RID: 331
			MembersPrimaryGroup,
			// Token: 0x0400014C RID: 332
			FileClosed,
			// Token: 0x0400014D RID: 333
			TooManyThreads,
			// Token: 0x0400014E RID: 334
			ThreadNotInProcess,
			// Token: 0x0400014F RID: 335
			TokenAlreadyInUse,
			// Token: 0x04000150 RID: 336
			PagefileQuotaExceeded,
			// Token: 0x04000151 RID: 337
			CommitmentLimit,
			// Token: 0x04000152 RID: 338
			InvalidImageLeFormat,
			// Token: 0x04000153 RID: 339
			InvalidImageNotMz,
			// Token: 0x04000154 RID: 340
			InvalidImageProtect,
			// Token: 0x04000155 RID: 341
			InvalidImageWin16,
			// Token: 0x04000156 RID: 342
			LogonServer,
			// Token: 0x04000157 RID: 343
			DifferenceAtDc,
			// Token: 0x04000158 RID: 344
			SynchronizationRequired,
			// Token: 0x04000159 RID: 345
			DllNotFound,
			// Token: 0x0400015A RID: 346
			IoPrivilegeFailed = 3221225783u,
			// Token: 0x0400015B RID: 347
			OrdinalNotFound,
			// Token: 0x0400015C RID: 348
			EntryPointNotFound,
			// Token: 0x0400015D RID: 349
			ControlCExit,
			// Token: 0x0400015E RID: 350
			PortNotSet = 3221226323u,
			// Token: 0x0400015F RID: 351
			DebuggerInactive,
			// Token: 0x04000160 RID: 352
			CallbackBypass = 3221226755u,
			// Token: 0x04000161 RID: 353
			PortClosed = 3221227264u,
			// Token: 0x04000162 RID: 354
			MessageLost,
			// Token: 0x04000163 RID: 355
			InvalidMessage,
			// Token: 0x04000164 RID: 356
			RequestCanceled,
			// Token: 0x04000165 RID: 357
			RecursiveDispatch,
			// Token: 0x04000166 RID: 358
			LpcReceiveBufferExpected,
			// Token: 0x04000167 RID: 359
			LpcInvalidConnectionUsage,
			// Token: 0x04000168 RID: 360
			LpcRequestsNotAllowed,
			// Token: 0x04000169 RID: 361
			ResourceInUse,
			// Token: 0x0400016A RID: 362
			ProcessIsProtected = 3221227282u,
			// Token: 0x0400016B RID: 363
			VolumeDirty = 3221227526u,
			// Token: 0x0400016C RID: 364
			FileCheckedOut = 3221227777u,
			// Token: 0x0400016D RID: 365
			CheckOutRequired,
			// Token: 0x0400016E RID: 366
			BadFileType,
			// Token: 0x0400016F RID: 367
			FileTooLarge,
			// Token: 0x04000170 RID: 368
			FormsAuthRequired,
			// Token: 0x04000171 RID: 369
			VirusInfected,
			// Token: 0x04000172 RID: 370
			VirusDeleted,
			// Token: 0x04000173 RID: 371
			TransactionalConflict = 3222863873u,
			// Token: 0x04000174 RID: 372
			InvalidTransaction,
			// Token: 0x04000175 RID: 373
			TransactionNotActive,
			// Token: 0x04000176 RID: 374
			TmInitializationFailed,
			// Token: 0x04000177 RID: 375
			RmNotActive,
			// Token: 0x04000178 RID: 376
			RmMetadataCorrupt,
			// Token: 0x04000179 RID: 377
			TransactionNotJoined,
			// Token: 0x0400017A RID: 378
			DirectoryNotRm,
			// Token: 0x0400017B RID: 379
			CouldNotResizeLog,
			// Token: 0x0400017C RID: 380
			TransactionsUnsupportedRemote,
			// Token: 0x0400017D RID: 381
			LogResizeInvalidSize,
			// Token: 0x0400017E RID: 382
			RemoteFileVersionMismatch,
			// Token: 0x0400017F RID: 383
			CrmProtocolAlreadyExists = 3222863887u,
			// Token: 0x04000180 RID: 384
			TransactionPropagationFailed,
			// Token: 0x04000181 RID: 385
			CrmProtocolNotFound,
			// Token: 0x04000182 RID: 386
			TransactionSuperiorExists,
			// Token: 0x04000183 RID: 387
			TransactionRequestNotValid,
			// Token: 0x04000184 RID: 388
			TransactionNotRequested,
			// Token: 0x04000185 RID: 389
			TransactionAlreadyAborted,
			// Token: 0x04000186 RID: 390
			TransactionAlreadyCommitted,
			// Token: 0x04000187 RID: 391
			TransactionInvalidMarshallBuffer,
			// Token: 0x04000188 RID: 392
			CurrentTransactionNotValid,
			// Token: 0x04000189 RID: 393
			LogGrowthFailed,
			// Token: 0x0400018A RID: 394
			ObjectNoLongerExists = 3222863905u,
			// Token: 0x0400018B RID: 395
			StreamMiniversionNotFound,
			// Token: 0x0400018C RID: 396
			StreamMiniversionNotValid,
			// Token: 0x0400018D RID: 397
			MiniversionInaccessibleFromSpecifiedTransaction,
			// Token: 0x0400018E RID: 398
			CantOpenMiniversionWithModifyIntent,
			// Token: 0x0400018F RID: 399
			CantCreateMoreStreamMiniversions,
			// Token: 0x04000190 RID: 400
			HandleNoLongerValid = 3222863912u,
			// Token: 0x04000191 RID: 401
			NoTxfMetadata,
			// Token: 0x04000192 RID: 402
			LogCorruptionDetected = 3222863920u,
			// Token: 0x04000193 RID: 403
			CantRecoverWithHandleOpen,
			// Token: 0x04000194 RID: 404
			RmDisconnected,
			// Token: 0x04000195 RID: 405
			EnlistmentNotSuperior,
			// Token: 0x04000196 RID: 406
			RecoveryNotNeeded,
			// Token: 0x04000197 RID: 407
			RmAlreadyStarted,
			// Token: 0x04000198 RID: 408
			FileIdentityNotPersistent,
			// Token: 0x04000199 RID: 409
			CantBreakTransactionalDependency,
			// Token: 0x0400019A RID: 410
			CantCrossRmBoundary,
			// Token: 0x0400019B RID: 411
			TxfDirNotEmpty,
			// Token: 0x0400019C RID: 412
			IndoubtTransactionsExist,
			// Token: 0x0400019D RID: 413
			TmVolatile,
			// Token: 0x0400019E RID: 414
			RollbackTimerExpired,
			// Token: 0x0400019F RID: 415
			TxfAttributeCorrupt,
			// Token: 0x040001A0 RID: 416
			EfsNotAllowedInTransaction,
			// Token: 0x040001A1 RID: 417
			TransactionalOpenNotAllowed,
			// Token: 0x040001A2 RID: 418
			TransactedMappingUnsupportedRemote,
			// Token: 0x040001A3 RID: 419
			TxfMetadataAlreadyPresent,
			// Token: 0x040001A4 RID: 420
			TransactionScopeCallbacksNotSet,
			// Token: 0x040001A5 RID: 421
			TransactionRequiredPromotion,
			// Token: 0x040001A6 RID: 422
			CannotExecuteFileInTransaction,
			// Token: 0x040001A7 RID: 423
			TransactionsNotFrozen,
			// Token: 0x040001A8 RID: 424
			MaximumNtStatus = 4294967295u
		}

		// Token: 0x0200001D RID: 29
		public struct UNICODE_STRING : IDisposable
		{
			// Token: 0x06000252 RID: 594 RVA: 0x00003A32 File Offset: 0x00001C32
			public UNICODE_STRING(string s)
			{
				this.Length = (ushort)(s.Length * 2);
				this.MaximumLength = this.Length + 2;
				this.buffer = Marshal.StringToHGlobalUni(s);
			}

			// Token: 0x06000253 RID: 595 RVA: 0x00003A5E File Offset: 0x00001C5E
			public void Dispose()
			{
				Marshal.FreeHGlobal(this.buffer);
				this.buffer = IntPtr.Zero;
			}

			// Token: 0x06000254 RID: 596 RVA: 0x00003A76 File Offset: 0x00001C76
			public override string ToString()
			{
				return Marshal.PtrToStringUni(this.buffer);
			}

			// Token: 0x040001A9 RID: 425
			public ushort Length;

			// Token: 0x040001AA RID: 426
			public ushort MaximumLength;

			// Token: 0x040001AB RID: 427
			private IntPtr buffer;
		}

		// Token: 0x0200001E RID: 30
		public enum ADJUST_PRIVILEGE_TYPE
		{
			// Token: 0x040001AD RID: 429
			AdjustCurrentProcess,
			// Token: 0x040001AE RID: 430
			AdjustCurrentThread
		}
	}
}
