namespace client
{
    using System;

    public enum AuthError
    {
        GAMESERVER_NOTEXIST = 9,
        LOGIN_ACCOUNTNOTEXIST = 1,
        LOGIN_BAN = 3,
        LOGIN_PASSWORDFAIL = 2,
        LOGIN_TIMEOUT = 12,
        LOGIN_VERSION = 4,
        REG_ACCOUNT_ERROR = 11,
        REG_ACCOUNTEXIST = 7,
        REG_ACCOUNTTOOLONG = 5,
        REG_INTERNALERROR = 8,
        REG_PASSWORDTOOLONG = 6,
        REG_REFEREE_ERROR = 10
    }
}

