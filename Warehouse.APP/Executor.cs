namespace Warehouse.APP;

internal static class Executor
{
    public static void Execute<TException>(Action action, Action<TException> errorAction) where TException : Exception
    {
        try
        {
            action();
        }
        catch (TException ex)
        {
            errorAction(ex);
        }
        catch (Exception ex)
        {
            LogError(ex.Message);
        }
    }
    
    public static void Execute(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            LogError(ex);
        }
    }

    private static void LogError(Exception exception) => 
        MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
}