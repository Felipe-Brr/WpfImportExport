namespace WpfImportExport.ViewModels
{
    public static class MainViewModel
    {
        public static ProjetoViewModel ProjetoViewModel { get; set; }
        static MainViewModel() 
        {
        
            ProjetoViewModel = new ProjetoViewModel();
        }
    }
}
