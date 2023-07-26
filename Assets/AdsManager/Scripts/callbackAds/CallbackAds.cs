public class CallbackAds
{
    public string LoadMessage { get; private set; }
    public string ErrorMessage { get; private set; }
    public void OnAdLoaded(string message="") {
        LoadMessage = message;
     }
    public void OnAdFailedToLoad(string error = "") { 
        ErrorMessage = error;
    }
}
