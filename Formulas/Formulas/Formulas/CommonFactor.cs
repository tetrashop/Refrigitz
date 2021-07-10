namespace Formulas
{
    internal static class CommonFactor
    {
        public static AddToTree.Tree CommonFactorFx(AddToTree.Tree Dummy1, AddToTree.Tree Dummy2)
        {
            AddToTree.Tree Dummy = CommonFactor.CommonFactorCalcultor(Dummy1, Dummy2);
            return Dummy;
        }

        private static AddToTree.Tree CommonFactorCalcultor(AddToTree.Tree Dummy1, AddToTree.Tree Dummy2)
        {
            AddToTree.Tree Dummy = new AddToTree.Tree("*", false);
            Dummy1.ThreadAccess = null;
            Dummy2.ThreadAccess = null;
            if (EqualToObject.IsEqualWithThreadConsiderationCommonly(Dummy1, Dummy2))
            {
                Dummy = Dummy1;
            }
            else
            {
                Dummy.SetLefTandRightCommonlySide(Dummy1, Dummy2);
                Dummy.LeftSideAccess.ThreadAccess = Dummy;
                Dummy.RightSideAccess.ThreadAccess = Dummy;
            }
            return Dummy;
        }
    }
}
