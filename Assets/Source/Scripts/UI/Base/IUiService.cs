namespace UI
{
    public interface IUiService
    {
        public T TryShowWindow<T>(int layer) where T : Window;
        
        public bool TryHideWindow<T>()  where T : Window;
    }
}