namespace BugProject
{
    public static class Constatnts
    {
        public static class Policies
        {
            public const string ForAdminOnly = "ForAdminOnly";
            public const string ForDev = "ForDev";
            public const string ForTester = "ForTester";
        }
    }

    enum BugRoles
    {
        Manager,
        Developer,
        Tester,
        PreimumUser,
        User
    }
}
