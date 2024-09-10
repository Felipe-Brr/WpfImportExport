namespace WpfImportExport.ViewModels
{
    public class MainViewModel
    {
        internal static ProjetoViewModel ProjetoViewModel { get; set; }
        public MainViewModel() 
        {
        
            ProjetoViewModel = new ProjetoViewModel();
        }
    }
}
