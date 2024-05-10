namespace ManageActivities.Repository.Utils;

public class RepositoryException(ERepositoryExceptionReason reason, string? stackTrace = null) : Exception
{
    public override string ToString()
    {
        return reason switch
        {
            ERepositoryExceptionReason.FindingAll => "Failed To Find All Entities",
            ERepositoryExceptionReason.FindingOne => "Failed To Find One Entity By Id",
            ERepositoryExceptionReason.Creating => "Failed To Create Entity",
            ERepositoryExceptionReason.Updating => "Failed To Update Entity",
            ERepositoryExceptionReason.Deleting => "Failed To Delete Entity",
            ERepositoryExceptionReason.ExistsById => "Failed To Find Entity That Exists By Id",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}