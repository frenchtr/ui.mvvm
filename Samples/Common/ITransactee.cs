namespace TravisRFrench.UI.MVVM.Samples.Common
{
    public interface ITransactee<out TConcrete>
        where TConcrete : class, ITransactee<TConcrete>
    {
        void ExecuteTransaction(ITransaction<TConcrete> transaction);
    }
}
