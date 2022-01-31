namespace DataObjects.Account.SessionCheck
{
    /** This DataObject contains a String  - Session's ID . For Checking Session Purposes.
     *  Input Parameter to AccountDal.SessionCheck(SessionCheckParameters parameters)
     */
    public class SessionCheckParameters
    {
        public string SessionId { get; set; }
    }
}
