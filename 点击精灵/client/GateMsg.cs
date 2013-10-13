namespace client
{
    using System;

    public enum GateMsg
    {
        GATE_MSG_NULL_ACTION,
        GATE_CMSG_PING,
        GATE_SMSG_PONG,
        GATE_CMSG_AUTH,
        GATE_SMSG_AUTH,
        GATE_CMSG_PLAYERREG,
        GATE_SMSG_PLAYERREG,
        GATE_CMSG_CRPTKEY,
        GATE_SMSG_CRPTKEY,
        GATE_CMSG_PLAYERREGUSER,
        GATE_SMSG_PLAYERREGUSER,
        GATE_CMSG_CHECKACCOUNT,
        GATE_SMSG_CHECKACCOUNT,
        GATE_MSG_TYPES_NUM
    }
}

