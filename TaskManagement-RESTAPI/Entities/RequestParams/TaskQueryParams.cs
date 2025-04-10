using System;

namespace TaskManagement_RESTAPI.Entities.RequestParams;

public class TaskQueryParams
{
    public int? UserId {get;set;}
    public bool? IsCompleted {get;set;}
    public bool? OverDue {get;set;}
    public string? SortBy {get;set;} = "DueDate";
    public bool Descending {get;set;} = false;

    public DateTime StartDate {get;set;}
    public DateTime EndDate {get;set;}

    public string? SearchTerm {get;set;}
    
    const int MaxPageSize = 50;
    public int PageNumber {get;set;} = 1;

    private int _pageSize = 10;
    public int PageSize {
        get 
        {
            return _pageSize;
        }set
        {
            _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
