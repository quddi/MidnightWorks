namespace UI
{
    public interface IUiService
    {
        T TryShowWindow<T>(int layer) where T : Window;
        
        bool TryHideWindow<T>()  where T : Window;

        void RegisterWindowsContainers(WindowsContainers windowsContainers);
    }
}