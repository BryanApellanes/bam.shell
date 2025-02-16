namespace Bam.Shell
{
    public class MenuSpec<CType, IType> : MenuSpec
    {
        public MenuSpec()
        { 
            this.ContainerType = typeof(CType);
            this.ItemAttributeType = typeof(IType);
        }
    }
}
