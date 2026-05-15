# GreyMagic x64 迁移计划

## 背景
Hearthbuddy 是 x86 .NET Framework 4.8.1 WPF 应用，读取 32 位炉石进程内存。
当前使用 GreyMagic.dll（混合模式，含 x86 原生代码和 FASM 汇编器）。

## 已完成
- SilverfishCards.dll 拆分（TypeLoader 不再 OOM）
- CardDB XmlReader + GC.Collect 优化
- app.config binding redirect
- LARGEADDRESSAWARE 确认已启用（4GB）

## GreyMagic 替换方案

### 核心思路
用纯 C# P/Invoke 替换 GreyMagic，实现 AnyCPU/x64 兼容。

### 关键文件
- 替换文件位置: `E:\Next\Hearthbuddy_Origin\Triton\Game\GreyMagic.cs`（已创建框架）
- Win32 API: `kernel32!ReadProcessMemory`, `WriteProcessMemory`, `VirtualAllocEx`, `CreateRemoteThread`
- FASM 汇编器: ManagedFasm 类（已提供 x86/x64 基础指令支持）

### 实现优先级

1. **Read<T>/ReadBytes/WriteBytes** - 核心内存读写（MonoClass.cs 使用最频繁）
2. **CallInjected<T>** - 远程代码执行（MonoMetadataReader.cs 依赖）
3. **AllocatedMemory** - VirtualAllocEx 包装（CallInjected 前置依赖）
4. **FrameLock/AcquireFrame** - 帧同步（Client.cs 依赖）
5. **GetProcAddress** - 远程导出表解析（ProcessHookManager 依赖）
6. **ManagedFasm** - 汇编指令生成（ProcessHookManager 依赖）
7. **PatchManager** - 内存补丁系统
8. **MarshalCache** - 结构体大小缓存

### 构建步骤
1. `dotnet build Hearthbuddy_Origin\Hearthbuddy.sln -c Release`
2. 修复所有 CS 错误（当前 ~206 个）
3. 改 csproj：`PlatformTarget` 改为 `AnyCPU`
4. 删除 GreyMagic 引用
5. 删除 Alib\GreyMagic_*.dll 内容项
6. 构建 DefaultRoutine + SilverfishCards（AnyCPU 兼容）
7. 集成测试

### 已知问题
- ManagedFasmExtensions.cs 使用 `Fasm.ManagedFasm`（命名空间需在 GreyMagic.cs 的顶层）
- Client.cs 的 `ReleaseFrame(reacquireAsHardLock: true)` 返回 IDisposable
- AllocatedMemory.Write<T>(offset, value) 有 3 参数重载
- CallInjected 有无返回值重载（2 参数 vs 3 参数）
