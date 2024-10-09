using System.ComponentModel.DataAnnotations;

public class CreateProductDto
{
    [Required(ErrorMessage = "Le code produit est obligatoire.")]
    public string code { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le nom du produit est obligatoire.")]
    public string name { get; set; } = string.Empty;

    [Required(ErrorMessage = "La description est obligatoire.")]
    public string description { get; set; } = string.Empty;

    [Required(ErrorMessage = "L'image est obligatoire.")]
    public string image { get; set; } = string.Empty;

    [Required(ErrorMessage = "La catégorie est obligatoire.")]
    public string category { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le prix est obligatoire.")]
    public decimal price { get; set; }

    [Required(ErrorMessage = "La quantité est obligatoire.")]
    public int quantity { get; set; }

    [Required(ErrorMessage = "La référence interne est obligatoire.")]
    public string internalReference { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le shell ID est obligatoire.")]
    public int shellId { get; set; }

    [Required(ErrorMessage = "Le statut d'inventaire est obligatoire.")]
    public string inventoryStatus { get; set; } = string.Empty;

    [Required(ErrorMessage = "La note est obligatoire.")]
    public int rating { get; set; }
}
