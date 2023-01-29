namespace Northgard.Interactor.Common.Mapper
{
    public interface IReadOnlyMapper<TS,TT>
    {
        public TT MapToTarget(TS source);
    }
}