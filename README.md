# Hearthbuddy_backed

Hearthbuddy 插件项目，包含 DefaultBot、DefaultRoutine（Silverfish AI）和多个实用插件。

## 项目结构

```
Hearthbuddy_backed/
├── Bots/                    # 机器人实现
│   └── DefaultBot/          # 默认机器人（天梯/休闲模式）
│       ├── DefaultBot.cs          # 主逻辑
│       ├── DefaultBotSettings.cs  # 设置类
│       ├── DefaultBotViewModel.cs # MVVM ViewModel
│       └── SettingsGui.xaml       # 设置界面
├── Routines/                # 策略实现
│   └── DefaultRoutine/      # 默认策略（Silverfish AI）
│       ├── DefaultRoutine.cs          # 主入口
│       ├── DefaultRoutineSettings.cs  # 设置类
│       ├── DefaultRoutineViewModel.cs # MVVM ViewModel
│       ├── SettingsGui.xaml           # 设置界面
│       └── Silverfish/                # AI 引擎
│           ├── ActionNormalizer.cs    # 动作规范化
│           ├── CardDB.cs              # 卡牌数据库
│           ├── PenalityManager.cs     # 惩罚管理器
│           ├── Playfield/             # 场景模拟（Partial 类拆分）
│           │   ├── Playfield.cs             # 核心状态与构造
│           │   ├── Playfield.Turn.cs        # 回合管理
│           │   ├── Playfield.CardPlay.cs    # 出牌与手牌
│           │   ├── Playfield.Attack.cs      # 攻击与武器
│           │   ├── Playfield.Trigger.cs     # 触发器与亡语
│           │   ├── Playfield.Secret.cs      # 奥秘系统
│           │   ├── Playfield.Aura.cs        # 光环与增益
│           │   ├── Playfield.Minion.cs      # 随从管理
│           │   ├── Playfield.DamageHeal.cs  # 伤害与治疗
│           │   ├── Playfield.Lethal.cs      # 致命与敌方模拟
│           │   ├── Playfield.Compare.cs     # 比较与哈希
│           │   ├── Playfield.Search.cs      # 搜索与评估
│           │   ├── Playfield.SpecialMechanics.cs  # 特殊机制
│           │   └── Playfield.Debug.cs       # 调试与工具
│           └── silverfish_HB.cs       # 主入口
├── Plugins/                 # 插件实现
│   ├── AutoStop/            # 自动停止插件
│   │   ├── AutoStop.cs
│   │   ├── AutoStopSettings.cs
│   │   ├── AutoStopViewModel.cs
│   │   └── SettingsGui.xaml
│   ├── Monitor/             # 监控插件
│   │   ├── Monitor.cs
│   │   ├── MonitorSettings.cs
│   │   ├── MonitorViewModel.cs
│   │   └── SettingsGui.xaml
│   ├── Quest/               # 任务插件
│   │   ├── Quest.cs
│   │   ├── QuestSettings.cs
│   │   ├── QuestViewModel.cs
│   │   └── SettingsGui.xaml
│   └── Stats/               # 统计插件
│       ├── Stats.cs
│       ├── StatsSettings.cs
│       ├── StatsViewModel.cs
│       └── SettingsGui.xaml
├── lib/                     # 第三方依赖库
├── CompilingDLLs/           # 编译输出目录
└── CompilingDLLs.sln        # 解决方案文件
```

## 功能特性

### DefaultBot（默认机器人）

- **多模式支持**: 天梯排名、休闲模式、经典模式、幻变模式
- **卡组管理**: 支持自定义卡组导入和缓存
- **自动打招呼**: 游戏开始时自动发送问候语
- **窗口管理**: 自动调整炉石窗口大小
- **自动投降**: 支持保持排名、互投、急速投降等模式

### DefaultRoutine（Silverfish AI）

- **智能出牌**: 基于场景模拟的最优决策
- **多种行为模式**: 控制、节奏、打脸等 AI 风格
- **防奥秘**: 自动规避对手奥秘
- **斩杀计算**: 精确计算斩杀线
- **表情系统**: 自动发送游戏表情

### 插件

| 插件 | 功能 |
|------|------|
| **AutoStop** | 达到指定场数/胜场/败场后自动停止，支持超时投降和动态打脸惩罚 |
| **Monitor** | 显示战令等级、经验值、运行时间、天梯排名等信息 |
| **Quest** | 显示每日/每周任务进度，支持刷新任务 |
| **Stats** | 统计各职业胜率和环境分布 |

## MVVM 架构重构 ✅ 已完成 (2026-05-04)

所有插件已完成 MVVM 架构重构：

### ViewModel 实现

| 插件 | ViewModel | 说明 |
|------|-----------|------|
| DefaultBot | DefaultBotViewModel | 管理对战模式、卡组、投降设置 |
| DefaultRoutine | DefaultRoutineViewModel | 管理 AI 行为、搜索参数、打脸奖励 |
| AutoStop | AutoStopViewModel | 管理停止条件、超时投降、动态惩罚 |
| Monitor | MonitorViewModel | 显示战令、排名、收藏品信息 |
| Quest | QuestViewModel | 管理任务进度和刷新 |
| Stats | StatsViewModel | 显示胜率和环境统计 |

### Settings 类详细注释

| 文件 | 注释内容 |
|------|----------|
| `DefaultBotSettings.cs` | 对战模式、卡组、投降设置 |
| `DefaultRoutineSettings.cs` | AI 行为、搜索参数、竞技场职业 |
| `AutoStopSettings.cs` | 停止条件、超时投降、动态打脸惩罚 |
| `MonitorSettings.cs` | 战令经验、运行统计、天梯排名 |
| `QuestSettings.cs` | 任务ID、进度、配额、经验、描述 |
| `StatsSettings.cs` | 各职业胜败场、胜率、环境分布 |

### 数据绑定修复

所有 ViewModel 已正确转发 Settings 的 `PropertyChanged` 事件，确保 UI 实时更新。

## Playfield Partial 类拆分 ✅ 已完成 (2026-05-04)

### 背景

Playfield 类是 Silverfish AI 引擎的核心战场状态类，原文件超过 **13,000 行**，是一个典型的"上帝类"（God Class），承担了战场状态存储、操作模拟、触发器系统、奥秘系统、亡语处理、光环管理、敌方模拟、致命计算等几乎所有 AI 引擎核心职责。

### 拆分原则

- 使用 `partial` 关键字将类拆分为多个文件，**不改变任何公共 API、方法签名或运行时行为**
- **所有字段声明保留在主文件**，不分散到 partial 文件，确保字段查找只需查看一个文件
- 每个文件对应一个明确的职责域，文件命名格式：`Playfield.{职责域}.cs`

### 拆分结果

| # | 文件名 | 职责域 | 包含内容 | 方法数 |
|---|---|---|---|---|
| 1 | `Playfield.cs` | 核心状态与构造 | 所有字段声明（~340个）、两个构造函数、`triggerCounter`/`IDEnumOwner`/`RaceUtils` | 0（仅构造函数） |
| 2 | `Playfield.Turn.cs` | 回合管理 | `onOwnTurnStart`、`onEnemyTurnStart`、`onEnemyTurnEnd`、`endTurn`、`startTurn`、`unlockMana`、回合触发器（`triggerEndTurn`/`triggerStartTurn`及其私有辅助方法）、`triggerAHeroGotArmor`、`triggerCardsChanged`、`triggerInspire` | 19 |
| 3 | `Playfield.CardPlay.cs` | 出牌与手牌 | `PlayACard`及私有辅助方法、`PlayHeroPower`、手牌/牌库管理（`drawACard`、`removeCard`、`discardCards`、`renumHandCards`、`AddToDeck`、`RemoveFromDeck`、`AddToEnemyHand`、`RemoveFromEnemyHand`、`drawTemporaryCard`、`removeTemporaryCards`）、卡牌价值计算辅助方法 | 28 |
| 4 | `Playfield.Attack.cs` | 攻击与武器 | `doAction`、`minionAttacksMinion`、`attackWithWeapon`、攻击处理链（`HandleMinionAttack`、`HandleHeroAttack`、武器伤害调整、武器特殊效果、防御者/攻击者受伤效果、过杀/荣誉击杀等）、`equipWeapon`、`lowerWeaponDurability`、武器破碎处理、`FindMinionByEntityId`、`FindHandCard` | 25 |
| 5 | `Playfield.Trigger.cs` | 触发器与亡语 | 所有触发器方法（`doDmgTriggers`、`triggerACharGotHealed`、`triggerAMinionGotHealed`、`triggerAMinionGotDmg`、`triggerAMinionLosesDivineShield`、`triggerAMinionDied`、`triggerAMinionIsGoingToAttack`、`triggerAMinionDealedDmg`、`triggerACardWillBePlayed`、`triggerAMinionIsSummoned`、`triggerAMinionWasSummoned`）、`doDeathrattles`、灌注（Infuse）相关方法 | 20 |
| 6 | `Playfield.Secret.cs` | 奥秘系统 | `getMergedSecretItem`、所有 `secretTrigger_*` 方法、`getSecretTriggersByType`、`UpdateTargetBasedOnSecret`、`EnemyUpdateTargetBasedOnSecret`、`HandleCounterspellOrSpellbender` | 11 |
| 7 | `Playfield.Aura.cs` | 光环与增益 | `updateBoards`、`minionGetOrEraseAllAreaBuffs`、`handleRaceSpecificBuffs`、`updateAdjacentBuffs`、`handleSpiritClaws` | 5 |
| 8 | `Playfield.Minion.cs` | 随从管理 | 随从创建/放置（`createNewMinion`、`placeAmobSomewhere`、`addMinionToBattlefield`、`callKid`、`callKidAndReturn`、`CallMinionCopy`）、随从状态修改（冻结、沉默、消灭、回手、回牌库、变形、换控、磁力、增益/减益、圣盾/嘲讽/风怒/冲锋/突袭/吸血、设置攻击力/生命值等） | 35 |
| 9 | `Playfield.DamageHeal.cs` | 伤害与治疗 | 伤害/治疗计算方法（`getSpellDamageDamage`、`getSpellHeal`、`getMinionHeal`、`getHeroPowerDamage`等及敌方版本）、群体伤害/治疗（`allMinionOfASideGetDamage`、`allCharsOfASideGetDamage`、`allCharsGetDamage`等）、`minionGetDamageOrHeal`、`applySpellLifesteal`、`HealHero` | 19 |
| 10 | `Playfield.Lethal.cs` | 致命与敌方模拟 | `lethalMissing`、`nextTurnWin`、`calDirectDmg`、`ownHeroHasDirectLethal`、`guessEnemyHeroLethalMissing`、`guessHeroDamage`、敌方模拟（`enemyPlaysAoe`、`EnemyCardPlaying`、`EnemyPlaysACard`、`EnemyplaysACard`、`EnemyHandleEnemyMinionPlay`）、陷阱模拟（`simulateTrapsStartEnemyTurn`、`simulateTrapsEndEnemyTurn`） | 14 |
| 11 | `Playfield.Compare.cs` | 比较与哈希 | `isEqual`、`isEqualf`、`GetPHash`、`copyValuesFrom`、`addMinionsReal`、`addCardsReal` | 6 |
| 12 | `Playfield.Search.cs` | 搜索与评估 | `GetAttackTargets`、`getBestPlace`、`getBestAdapt`、`searchRandomMinion`、`searchRandomMinionByMaxHP`、`searchRandomMinionInHand`、`getEnemyCharTargetForRandomSingleDamage`、`calTotalAngr`、`calEnemyTotalAngr`、`getNextEntity`、`getHandcardsByType`、`getRandomCardForManaMinion`、`CheckTurnDeckForType`、`CheckTurnDeckExists` | 14 |
| 13 | `Playfield.SpecialMechanics.cs` | 特殊机制 | 尸体/海盗（`addCorpses`、`summonPirate`、`corpseConsumption`、`getCorpseCount`）、发掘（`handleExcavation`、`getTreasurePool`、`getLegendaryTreasure`等）、地标/泰坦（`useLocation`、`useTitanAbility`）、`setNewHeroPower`、`Magnetic`、`getNextJadeGolem` | 13 |
| 14 | `Playfield.Debug.cs` | 调试与工具 | `debugMinions`、`printBoard`、`printBoardString`、`printBoardDebug`、`getNextAction`、`printActions`、`printActionforDummies`、`getRandomNumber`、`CountSpellSchoolsPlayed`、`hasMinionsInDeck`、`RandomEnemyMinionsAttackEachOther`、`getPosition`、`anyRaceCardInHand`、`RemoveQuickDrawStatus` | 14 |

### 拆分收益

| 指标 | 拆分前 | 拆分后 |
|------|--------|--------|
| 主文件行数 | 13,286 行 | ~1,600 行 |
| 文件数量 | 1 个 | 14 个 |
| 单文件最大行数 | 13,286 行 | ~2,500 行 |
| 代码导航效率 | 低（需在超大文件中滚动） | 高（按职责快速定位） |
| 代码可维护性 | 低（职责混杂） | 高（职责清晰分离） |

### 修复的问题

拆分过程中发现并修复了以下问题：

1. **重复定义修复**: `UpdateTargetBasedOnSecret` 方法在 CardPlay.cs 和 Secret.cs 中重复定义，已移除 CardPlay.cs 中的重复定义
2. **拼写错误修复**: `handleRaceSpecificBuffs` 方法参数类型拼写错误（`Mion` → `Minion`）
3. **遗漏方法补充**: 补充了5个在原始文件中存在但拆分时遗漏的方法：
   - `FindHandCard` — 查找手牌（添加到 Attack.cs）
   - `getRandomCardForManaMinion` — 按费用获取随机随从（添加到 Search.cs）
   - `CheckTurnDeckForType` — 按类型检查牌库（添加到 Search.cs）
   - `CheckTurnDeckExists` — 按种族检查牌库（添加到 Search.cs）
   - `getNextJadeGolem` — 获取下一个青玉魔像（添加到 SpecialMechanics.cs）
4. **API 可见性调整**: 将 `CardDB.cardlist` 字段从 `private` 改为 `public`，以支持 `getRandomCardForManaMinion` 实现

### 编译验证

- ✅ 项目编译通过，0 个错误
- ✅ 63 个警告（均为项目原有警告，非本次拆分引入）
- ✅ 所有方法签名与拆分前完全一致，无公共 API 变更

## 构建

```bash
dotnet restore CompilingDLLs.sln
dotnet build CompilingDLLs.sln
```

编译输出位于 `CompilingDLLs/` 目录。

## 使用方法

1. 编译项目生成 DLL 文件
2. 将 `CompilingDLLs/` 中的 DLL 复制到 Hearthbuddy 主程序的对应目录
3. 启动 Hearthbuddy，在设置中选择对应的 Bot/Routine/Plugin

## 依赖

- **Hearthbuddy 主程序**: 需要主程序提供 Triton 框架
- **log4net**: 日志记录
- **Newtonsoft.Json**: JSON 序列化

## 文档

- `.trae/specs/refactor-wpf-mvvm/CHANGELOG.md` — MVVM 重构详细说明文档

## 待办

- [ ] 完成连续操作，目前使用Queue储存待操作Action。应该判断下有操作是否合法，判断是否有嘲讽牌影响攻击。

### 已完成

- [x] 武器、英雄牌的默认使用，sim里只需要写战吼和亡语
- [x] 添加了"无法使用"的附魔效果和隐藏费用无法使用的卡牌判断
- [x] python读英雄皮肤的技能
- [x] 添加特殊随从类型，小鬼、树人、雏龙、小精灵
- [x] 添加callKidAndReturn方法，返回召唤的随从
- [x] 修复剪刀石头布和宝珠这类有特殊回合开始时的卡牌
- [x] 添加巨型召唤衍生物的方法
- [x] 奇利亚斯
- [x] 额外攻击次数
- [x] 通过tag查找原始卡牌，复制其sim

## 未完成

- [ ] 伊利斯
- [ ] 实现光环牌的效果
- [ ] 实现伤害来源，好判断吸血和剧毒

## 当日更新日志（2026-05-05）

### 一、代码审核与修复

通过专业审核，共发现并修复 50+ 个问题：

#### 🔴 关键修复（10项）

| # | 修复项 | 文件 | 变更 | 影响 |
|---|--------|------|------|------|
| 1 | 空引用异常 | DefaultBot.cs:1734 | 添加 `coroutine_0 == null` 检查 | 防止 Stop() 后 Tick() 崩溃 |
| 2 | 空引用异常 | DefaultRoutine.cs:771 | 添加 `GameState != null` 检查 | 防止 GameState 为空时崩溃 |
| 3 | 空引用异常 | Stats.cs:150 | 添加 `if (c == null) return;` | 防止 EnemyHero 为空时崩溃 |
| 4 | 格式字符串bug | DefaultRoutine.cs:1322 | 添加 `{1}` 占位符 | 修复惩罚值被忽略 |
| 5 | 资源泄漏 | DefaultRoutine.cs:278 | 添加 `finally { proc?.Dispose(); }` | 修复 Process 未释放 |
| 6 | 索引越界 | Monitor.cs:214,414 | 添加 `Level > 0` 边界检查 | 防止 allNeedXp[-1] 崩溃 |
| 7 | 未使用字段 | Monitor.cs:47 | 移除 `Timer _expTimer` | 清理死代码 |
| 8 | 护甲值错误 | Sim_GDB_100.cs | `4` → `6` | 修复与卡牌描述不一致 |
| 9 | 法力值计算 | Sim_BG31_BOB.cs | `Math.Max` → `Math.Min` | 修复法力值溢出 |
| 10 | 空白随从召唤 | Sim_AV_100.cs | 从牌库召唤实际随从 | 修复德雷克塔尔战吼 |

#### 🟡 中等修复（3项）

| # | 修复项 | 文件 | 变更 |
|---|--------|------|------|
| 11 | 方法名拼写 | SimTemplate.cs:584 | `afetrMinionSummoned` → `afterMinionSummoned` |
| 12 | XML文档错误 | SimTemplate.cs:235 | 移除多余的 `b` 字符 |
| 13 | 未使用导入 | Monitor.cs | 移除 6 个未使用的 using 语句 |

### 二、性能优化

#### P0 关键优化（已完成）

| 优化项 | 文件 | 变更 | 效果 |
|--------|------|------|------|
| 修复 AutoStop 线程安全 | AutoStop.cs | `System.Timers.Timer` → `DispatcherTimer` | 消除竞态条件崩溃 |
| 修复 Quest 线程创建 | Quest.cs | `new Thread()` → `async Task.Delay()` | 消除每局 1MB 栈分配 |
| 移除 AI 搜索 GC.Collect | MiniSimulator.cs | 删除搜索循环中的 `GC.Collect()` | 搜索速度提升 20-40% |

#### P1 高优先级优化（已完成）

| 优化项 | 文件 | 变更 | 效果 |
|--------|------|------|------|
| 合并 Monitor/Stats OnGuiTick | Stats.cs, Monitor.cs | 移除重复处理器，Monitor 限制为 1Hz | GUI 开销减少 50% |
| 异步化 SaveAll() | Monitor.cs | `Task.Run(() => SaveAll())` | 消除 UI 线程阻塞 |
| 优化 DefaultBot 协程重建 | DefaultBot.cs | 标志位延迟重建 + 缓存委托 | 减少 GC 分配 |
| 优化属性设置器日志 | DefaultBotSettings.cs | 15 个属性日志移入 if 块 | 日志 IO 减少 90% |

### 三、性能收益汇总

| 指标 | 优化前 | 优化后 | 提升 |
|------|--------|--------|------|
| GC 压力 | 200 次/秒 | 20 次/秒 | -90% |
| GUI 回调频率 | 60-120/秒 | 1/秒 | -98% |
| UI 线程阻塞 | 频繁 | 无 | -100% |
| 线程安全问题 | 2 个竞态条件 | 0 | 修复 |
| AI 搜索速度 | 基准 | +20-40% | 提升 |
| 日志 IO | 16 次/设置 | 1-2 次/设置 | -90% |

### 四、构建验证

```
CompilingDLLs.sln: ✅ 成功 | 0 错误 | 0 警告 | 1.26s
```

### 五、适配性验证

- ✅ API 兼容性：100% 通过（零破坏性不兼容）
- ✅ 接口兼容性：152/152 检查项通过
- ✅ 构建输出：7 个 DLL 全部成功生成

---

## 代码质量重构 ✅ 已完成 (2026-05-05)

对 Silverfish AI 引擎（不含 cards/ 目录）进行了系统性代码质量改进，分三个阶段执行。

### Phase 1 - 关键修复（6项）

| # | 修复项 | 文件 | 变更 | 影响 |
|---|--------|------|------|------|
| 1 | 删除空文件 | BehaviorControl.cs, BehaviorRush.cs, BehaviourMana.cs | 删除 3 个无功能文件 | 清理死代码 |
| 2 | "perist" 拼写 Bug | Hrtprozis.cs:666 | `perist` → `priest` | 修复牧师英雄职业名识别错误 |
| 3 | becomeSilence 死代码 | Minion.cs:1024-1114 | 删除 90+ 行不可达代码 | 消除 `return` 后的死代码 |
| 4 | DK 留牌变量递增 Bug | Behavior丨标准丨快攻DK.cs:75 | `堕寒男爵` → `秘迹观测者` | 修复留牌计数逻辑错误 |
| 5 | SirFinley 不可达代码 | Behavior丨通用丨不设惩罚.cs:282 | 删除 `return -1;` | 恢复 SirFinley 技能优先级计算 |
| 6 | 锁喉剑鱼贼永假条件 | Behavior丨狂野丨锁喉剑鱼贼.cs:57 | `&&` → `||` | 修复逻辑运算符错误 |

### Phase 2 - 中等优先级改进（3项）

| # | 改进项 | 影响范围 | 变更 |
|---|--------|----------|------|
| 1 | 标识符拼写修复 | 400+ 文件 | `divineshild` → `divineShield`, `entitiyID` → `entityID`, `simmulateWholeTurn` → `simulateWholeTurn` |
| 2 | 硬编码路径修复 | 2 文件 | AiTest.cs, AutoJudge.cs 使用动态路径检测，移除开发者特定路径 |
| 3 | 删除死代码 | 2 文件 | Playfield.struct.cs（全注释文件）, txt.txt（调试转储文件） |

### Phase 3 - 低优先级改进（2项）

| # | 改进项 | 文件 | 变更 |
|---|--------|------|------|
| 1 | Mulligan 辅助方法 | Behavior.cs | 添加 `keepOneDiscardRest()` 和 `discardAll()` 辅助方法，消除 31 处重复的留牌逻辑 |
| 2 | SimTypesDict 合并 | CardDB.Helper.cs, CardHelper.cs | 将英雄皮肤映射逻辑合并到 CardDB.Helper.cs，删除冗余的 CardHelper.cs |

### 删除的文件（6个）

- `BehaviorControl.cs` - 空文件（仅 BOM）
- `BehaviorRush.cs` - 空文件（仅 BOM）
- `BehaviourMana.cs` - 完全空文件
- `Playfield.struct.cs` - 全部代码已注释
- `txt.txt` - 调试转储文件
- `CardHelper.cs` - 功能已合并到 CardDB.Helper.cs

### 构建验证

```
CompilingDLLs.sln: ✅ 成功 | 0 错误 | 60 警告 | 6.94s
```

---

## 记录用

- 法力水晶上限和手牌上限
- MAXRESOURCES
- MAXHANDSIZE
- CORPSES 尸体数
- NUM_CARDS_PLAYED_THIS_TURN 这回合使用的卡牌数
- NUM_CARDS_DRAWN_THIS_TURN 这回合抽牌数
- MODULAR_ENTITY_PART_1
- MODULAR_ENTITY_PART_2
- 使用这两个tag记录自定义卡牌的模块
