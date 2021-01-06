namespace Keepr.Models
{
  public class Keep
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Keeps { get; set; }
    public int Views { get; set; }
    public int Shares { get; set; }
    public string Img { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
  }
  // In here you will add the ViewModel for the VaultKeeps : Prodcuct here with a public prop int of VaultKeepsId from the relationship table
  public class VaultKeepsViewModel : Keep
  {
    public int VaultKeepId { get; set; }
  }
}