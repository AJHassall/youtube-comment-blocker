public class BlockedUser
{
    public string Name {get; set;}
    public Guid Id {get; set;}
    public BlockedUser(){

        Name = "";
        Id = Guid.NewGuid();
    }
}