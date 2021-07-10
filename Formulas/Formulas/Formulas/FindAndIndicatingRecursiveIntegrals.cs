namespace Formulas
{
    internal static class FindAndIndicatingRecursiveIntegrals
    {
        public static AddToTree.Tree FindAndIndicatingRecursiveIntegralsFx(AddToTree.Tree Node, AddToTree.Tree Dummy, AddToTreeTreeLinkList INTEGRAL, AddToTreeTreeLinkList ANSWER, out bool IntegralA, out float Queficient)
        {
            Dummy = FindAndIndicatingRecursiveIntegrals.FindAndIndicatingRecursiveIntegralsFxLinrearFx(Node, Dummy, out IntegralA, out Queficient);
            return Dummy;
        }
        private static AddToTree.Tree FindAndIndicatingRecursiveIntegralsFxLinrearFx(AddToTree.Tree Node, AddToTree.Tree MULTOW, out bool IntegralA, out float Queficient)
        {
            if (MULTOW.SampleAccess == "*")
            {
                //if (INTEGRALS.FINDTreeWithThreadConsideration(MULTOW.LeftSideAccess))
                if (EqualToObject.IsEqualWithThreadConsiderationCommonly(MULTOW.LeftSideAccess, Node))
                {
                    Queficient = (float)System.Convert.ToDouble(MULTOW.RightSideAccess.SampleAccess);
                    Queficient = Queficient * (-1);
                    MULTOW = MULTOW.LeftSideAccess;
                    MULTOW.ThreadAccess = null;
                    IntegralA = true;
                }
                else
                    //if (INTEGRALS.FINDTreeWithThreadConsideration(MULTOW.RightSideAccess))
                    if (EqualToObject.IsEqualWithThreadConsiderationCommonly(MULTOW.RightSideAccess, Node))
                {
                    Queficient = (float)System.Convert.ToDouble(MULTOW.LeftSideAccess.SampleAccess);
                    Queficient = Queficient * (-1);
                    MULTOW = MULTOW.RightSideAccess;
                    MULTOW.ThreadAccess = null;
                    IntegralA = true;
                }
                else
                {
                    IntegralA = false;
                    Queficient = 0;
                }
            }
            else
            {
                IntegralA = false;
                Queficient = 0;
            }
            return MULTOW;
        }
    }
}
