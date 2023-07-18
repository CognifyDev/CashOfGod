﻿global using Hazel;
global using HarmonyLib;
using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using COG.Config;
using COG.Config.Impl;
using COG.Listener;
using COG.Listener.Impl;
using COG.Role.Impl;
using COG.UI.SidebarText;
using COG.UI.SidebarText.Impl;
using COG.Utils;
using Reactor;
using Reactor.Utilities.Extensions;

namespace COG;

[BepInAutoPlugin(PluginGuid, PluginName)]
[BepInIncompatibility("com.emptybottle.townofhost")]
[BepInIncompatibility("me.eisbison.theotherroles")]
[BepInIncompatibility("me.yukieiji.extremeroles")]
[BepInIncompatibility("jp.ykundesu.supernewroles")]
[BepInIncompatibility("com.tugaru.TownOfPlus")]
[BepInDependency(ReactorPlugin.Id)]
[BepInProcess("Among Us.exe")]
public partial class Main : BasePlugin
{
    public const string PluginName = "Clash Of Gods";
    public const string PluginGuid = "top.cog.clashofgods";
    public const string PluginVersion = "1.0.0";
    public Harmony harmony { get; } = new(PluginGuid);
    public const string DisplayName = "ClashOfGods";

    public static BepInEx.Logging.ManualLogSource Logger;

    public static Main Instance { get; private set; }


    /// <summary>
    /// 插件的启动方法
    /// </summary>
    public override void Load()
    {
        Instance = this;

        Logger = BepInEx.Logging.Logger.CreateLogSource(DisplayName + "   ");
        Logger.LogInfo("Loading...");
        
        // 添加依赖
        ResourceUtils.WriteToFileFromResource(
            "BepInEx/core/YamlDotNet.dll", 
            "COG.Resources.InDLL.Depends.YamlDotNet.dll");
        ResourceUtils.WriteToFileFromResource(
            "BepInEx/core/YamlDotNet.xml", 
            "COG.Resources.InDLL.Depends.YamlDotNet.xml");
        
        ListenerManager.GetManager().RegisterListeners(new IListener[]
        {
            new CommandListener(), 
            new GameListener(), 
            new VersionShowerListener(), 
            new PlayerListener(),
            new OptionListener()
        });
        
        SidebarTextManager.GetManager().RegisterSidebarTexts(new SidebarText[]
        {
            new OriginalSettings(),
            new ModSettings()
        });
        
        Role.RoleManager.GetManager().RegisterRoles(new Role.Role[]
        {
            new Crewmate(),
            new Impostor(),
            new Jester()
        });

        harmony.PatchAll();
    }
}