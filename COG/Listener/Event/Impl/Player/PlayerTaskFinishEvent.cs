using Il2CppSystem.Collections.Generic;

namespace COG.Listener.Event.Impl.Player;

/// <summary>
/// һ������������ʱ�����¼���ִ��
/// </summary>
public class PlayerTaskFinishEvent : PlayerEvent
{
    /// <summary>
    /// �����������
    /// </summary>
    public uint Index { get; }

    public PlayerTaskFinishEvent(PlayerControl player, uint idx) : base(player)
    {
        Index = idx;
    }
}