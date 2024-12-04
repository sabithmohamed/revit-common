namespace Idibri.RevitPlugin.Common.Infrastructure
{
    public class ViewModelBase : ModelBase
    {
        protected void UpdateCommands(params RelayCommand[] commands)
        {
            foreach (RelayCommand command in commands)
            {
                command.UpdateCanExecuteState();
            }
        }
    }
}
