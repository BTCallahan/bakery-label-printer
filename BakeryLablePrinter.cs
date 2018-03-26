using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System;
/*
 * Brian callahan, 11/8/2017
 *
 * This is an object oriented approach to storing information about the
 * products that the baker has to offer. My reson for creating this are to
 * standerise the names of the products - no more seeing Cherry Pie 8IN
 * and a Pie 8 IN Pumpkin, and well as eliminating duplication entries.
 * Further improvments are the ability to flag a product if it has dairy
 * gluten, and/or nuts, for the benfit of the customer. There are still a
 * numner of unimplemented feature, such as the barcode.
 */
public class ProductType{

  static public ProductType rollKaiser = new ProductType("Kaiser Rolls", true);
  static public ProductType rollTelara = new ProductType("Telara Rolls", true);
  static public ProductType rollSandwich = new ProductType("Sandwich Rolls", true);
  static public ProductType cookies = new ProductType("Cookies", true);
  static public ProductType cookiesJumbo = new ProductType("Jumbo Cookies", true);
  static public ProductType puddingRing = new ProductType("Pudding Ring");
  static public ProductType brownie = new ProductType("Brownie", true);
  static public ProductType cakeRound = new ProductType("Cake", false, true);
  static public ProductType pie = new ProductType("Pie", false, true, true);

	public string productTypeName;
  public bool canHaveCount = false;
  public bool canHaveLayers = false;
  public bool canHaveWidth = false;

	public ProductType(string name){
		this.productTypeName = name;
	}

  public ProductType(string name, bool canHaveCount=false, bool canHaveLayers=false, bool canHaveWidth=false){
    this.productTypeName = name;
    this.canHaveCount = canHaveCount;
    this.canHaveLayers = canHaveLayers;
    this.canHaveWidth = canHaveWidth;
  }

}

public abstract class BaseIngredient{

  public abstract string GetName{
    get;
  }

  public abstract bool ContainsLactose{
    get;
  }

  public abstract bool ContainsGlutin{
    get;
  }

  public abstract bool ContainsNuts{
    get;
  }
}

public class Flavor{

  public static Flavor plain = new Flavor("");
  public static Flavor chesse = new Flavor("Cheese");
  public static Flavor strawberry = new Flavor("Strawberry");
  public static Flavor raspberry = new Flavor("Raspberry");
  public static Flavor blueberry = new Flavor("Blueberry");
  public static Flavor chocolate = new Flavor("Chocolate");
  public static Flavor doubleChocolate = new Flavor("Double Chocolate");

  public string flavorName;

  public Flavor(string name){
    this.flavorName = name;
  }
}

public class Ingredient : BaseIngredient{

  static public Ingredient flour = new Ingredient("flour", false, true);
  static public Ingredient water = new Ingredient("water");
  static public Ingredient salt = new Ingredient("salt");
  static public Ingredient yeast = new Ingredient("yeast");
  static public Ingredient sugar = new Ingredient("sugar");

  static public Ingredient wheatFlour = new Ingredient("wheat flour", false, true);
  static public Ingredient niacin = new Ingredient("niacin");
  static public Ingredient iron = new Ingredient("iron");
  static public Ingredient ascorbicAcid = new Ingredient("absorbic acid");
  static public Ingredient folicAcid = new Ingredient("folic acid");
  static public Ingredient riboflavin = new Ingredient("riboflavin");

  static public Ingredient maltedBarlyFlour = new Ingredient("malted barly flour");
  static public Ingredient ironAsFerrousSlufate = new Ingredient("iron as ferrous sulfate");
  static public Ingredient thiamineMononitrate = new Ingredient("thiamine mononitrate");
  static public Ingredient diacetylTartaricAcidEsters = new Ingredient("diacetyl tartaric acid esters of mono-and diglycerides (datem)");
  static public Ingredient enzymes = new Ingredient("enzymes");
  static public Ingredient soybeanOil = new Ingredient("soybean oil");
  static public Ingredient palmOil = new Ingredient("palm oil");
  static public Ingredient highFructoseCornSyrup = new Ingredient("high fructose corn syrup");
  static public Ingredient cornSyrupSolids = new Ingredient("corn syrup solids");
  static public Ingredient maltodextrin = new Ingredient("maltodextrin");

  private string ingredientName;
  private bool containsLactose;
  private bool containsGlutin;
  private bool containsNuts;

  public Ingredient(string name, bool containsLactose=false, bool containsGlutin=false, bool containsNuts=false){
    this.ingredientName = name;
    this.containsLactose = containsLactose;
    this.containsGlutin = containsGlutin;
    this.containsNuts = containsNuts;
  }

  public override bool ContainsLactose{
    get{
      return containsLactose;
    }
  }

  public override bool ContainsGlutin{
    get{
      return containsGlutin;
    }
  }

  public override bool ContainsNuts{
    get{
      return containsNuts;
    }
  }

  public override string GetName{
    get{
      return ingredientName;
    }
  }
}

public class SuperIngredient : BaseIngredient{

  static public SuperIngredient frenchBase = new SuperIngredient("french base",
    Ingredient.wheatFlour, Ingredient.diacetylTartaricAcidEsters,
    Ingredient.enzymes, Ingredient.soybeanOil, Ingredient.ascorbicAcid);
  static public SuperIngredient enrichedUnbleachedWheatFlour = new SuperIngredient("enriched unbleached wheat flour", Ingredient.wheatFlour,
    Ingredient.maltedBarlyFlour, Ingredient.niacin, Ingredient.iron,
    Ingredient.thiamineMononitrate, Ingredient.riboflavin,
    Ingredient.folicAcid);

  private string ingredientName;
  private Ingredient[] ingredients;

  public SuperIngredient(string name, params Ingredient[] ingredients){
    this.ingredientName = name;
    this.ingredients = ingredients;
  }

  public override string GetName{
    get{
      StringBuilder sb = new StringBuilder(ingredientName + " (");

      for(int i = 0; i < ingredients.Length; i++){
        if(i > 0){
          sb.Append(", ");
        }
        sb.Append(ingredients[i].GetName);
      }
      sb.Append(")");
      return sb.ToString();
    }
  }

  public override bool ContainsLactose{
    get{
      for(int i = 0; i < ingredients.Length; i++){
        if(ingredients[i].ContainsLactose){
          return true;
        }
      }
      return false;
    }
  }

  public override bool ContainsGlutin{
    get{
      for(int i = 0; i < ingredients.Length; i++){
        if(ingredients[i].ContainsGlutin){
          return true;
        }
      }
      return false;
    }
  }

  public override bool ContainsNuts{
    get{
      for(int i = 0; i < ingredients.Length; i++){
        if(ingredients[i].ContainsNuts){
          return true;
        }
      }
      return false;
    }
  }
}



public class BakeryProduct{

	public int IDNumber;
	public string productName;
  public string labelToPrint;
	public int count;//used for brownies, rolls, cookies, muffins, ect
  private int shelfLifeInDays;
  private double doubleShelfLifeInDAys;
	public int layers;//useed for cakes
	public int width;//used for cakes and pies
	private float price;
  private float weight;
	public ProductType productType;
	public Flavor flavor;
	private BaseIngredient[] ingredients;
  public string fullProductName;
  public string fullProductNameLower;
  public string weightAsText;
  public string ingredientsAsText;
  public string priceAsString;
  public bool containsLactose;
  public bool containsNuts;
  public bool containsGlutin;

  private static CultureInfo ci = new CultureInfo("en-US");

	public BakeryProduct(string name, int idNumber, int shelfLifeInDays,
  float price, int count, float weight, int width, int layers,
  ProductType productType, Flavor flavor, params BaseIngredient[] ingredients){
		this.productName = name;
    this.IDNumber = idNumber;
    this.shelfLifeInDays = shelfLifeInDays;
    this.doubleShelfLifeInDAys = (double)shelfLifeInDays;
		this.price = price;
    this.weight = weight;
		this.count = count;
		this.width = width;
		this.layers = layers;
		this.productType = productType;
		this.flavor = flavor;
		this.ingredients = ingredients;
    this.fullProductName = AssignName();
    this.containsLactose = ContainsLactose();
    this.containsNuts = ContainsNuts();
    this.containsGlutin = ContainsGlutin();
    this.ingredientsAsText = GetIngredients();
    this.priceAsString = string.Format("Total Price: {0}", price.ToString("C", ci));
    this.fullProductNameLower = this.priceAsString.ToLower();
    this.labelToPrint = PrintCompleteLabel();
	}

  //constructor for cakes
	public BakeryProduct(string name, int idNumber, int shelfLifeInDays,
    float price, float weight, int width, int layers, Flavor flavor,
    params BaseIngredient[] ingredients) : this(name, idNumber,
    shelfLifeInDays, price, -1, weight, width, layers, ProductType.cakeRound,
    flavor, ingredients)
  {
	}

  //constructor for pies
	public BakeryProduct(string name, int idNumber, int shelfLifeInDays,
    float price, float weight, int width, Flavor flavor,
    params BaseIngredient[] ingredients) : this(name, idNumber,
    shelfLifeInDays, price, -1, weight, width, -1, ProductType.pie, flavor,
    ingredients)
  {
	}

  //constructor for rolls, cookies, muffins, ect.
  public BakeryProduct(string name, int idNumber, int shelfLifeInDays,
    float price, int count, float weight, ProductType productType,
    Flavor flavor,  params BaseIngredient[] ingredients) : this(name, idNumber,
    shelfLifeInDays, price, count, weight, -1, -1, productType, flavor,
    ingredients)
  {
  }

	public string AssignName(){
    StringBuilder sb = new StringBuilder("");

		if(width > 0){
      sb.Append(string.Format("{0} Inch ", width));
		}

		if(layers > 0){
      sb.Append(string.Format("{0} Layer ", layers));
		}

		if(flavor.flavorName != ""){
      sb.Append(flavor.flavorName + " ");
		}

    sb.Append(productType.productTypeName);

		if(count > 0){
      sb.Append(string.Format("{0} Count", count));
		}
		return sb.ToString();
	}

	public string GetIngredients(){
    StringBuilder sb = new StringBuilder();

		for(int i = 0; i < ingredients.Length; i++){
			if(i > 0){
        sb.Append(" ");
			}
      sb.Append(ingredients[i].GetName);
		}
		return sb.ToString();
	}

  public string GetWeight(){

    float ounces = weight * 0.035274f;
    float ouncesRemainder = ounces % 16f;
    float pounds = ounces - ouncesRemainder;

    return string.Format("NET WT. {0.0}lb {1.0}oz ({2}g)", pounds,
    ouncesRemainder, weight);
  }

	public bool ContainsLactose(){
		for(int i = 0; i < ingredients.Length; i++){
			if(ingredients[i].ContainsLactose){
				return true;
			}
		}
		return false;
	}

	public bool ContainsGlutin(){
		for(int i = 0; i < ingredients.Length; i++){
			if(ingredients[i].ContainsGlutin){
				return true;
			}
		}
		return false;
	}

	public bool ContainsNuts(){
		for(int i = 0; i < ingredients.Length; i++){
	   if(ingredients[i].ContainsNuts){
       return true;
			}
		}
		return false;
	}

  private static string dateTimeFormat = "MMMM d y";

  public string PrintExperationDate{
    get{
      DateTime dt = DateTime.Today;

      StringBuilder sb = new StringBuilder(string.Format("Packed on: {0}\n", dt.ToString(dateTimeFormat)));

      dt.AddDays(doubleShelfLifeInDAys);

      sb.Append(string.Format("Sell by: {0}", dt.ToString(dateTimeFormat)));

      return sb.ToString();
    }
  }

  public string ModifyAndPrintExperationDate(double packed=0.0, double shelfLife=0.0){
    DateTime dt = DateTime.Today;

    dt.AddDays(packed);

    StringBuilder sb = new StringBuilder(string.Format("{0}\n", dt.ToString(dateTimeFormat)));

    dt.AddDays(-packed + doubleShelfLifeInDAys + shelfLife);

    sb.Append(string.Format("Sell by: {0}", dt.ToString(dateTimeFormat)));

    return sb.ToString();
  }

  public string PrintCompleteLabel(){

    StringBuilder sb = new StringBuilder(fullProductName + "\n\n" + priceAsString + "\n\n" + "{0}" + "\n\n" + weightAsText + "\n\n" + ingredientsAsText);
    if(containsNuts)
    {
      sb.Append("\nCONTAINS NUTS");
    }
    if(containsGlutin)
    {
      sb.Append("\nCONTAINS GLUTIN");
    }
    if(containsLactose)
    {
      sb.Append("\nCONTAINS LACTOSE");
    }

    return sb.ToString();
  }

  public bool IsValidItem{
    get{
      return price != 99.99f;
    }
  }

  public bool CheckCountForSearch(int count){
    return productType.canHaveCount && count == this.count;
  }

  public bool CheckLayersForSearch(int layers){
    return productType.canHaveLayers && layers == this.layers;
  }

  public bool CheckWidthForSearch(int width){
    return productType.canHaveWidth && layers == this.width;
  }

}

class Bakery {
  //this currently has only one item - I'll add some more when I have enough ingredients
  public static Dictionary<int, BakeryProduct> CreateDictionary{
    get{
      Dictionary<int, BakeryProduct> d = new Dictionary<int, BakeryProduct>();

      d.Add(25002, new BakeryProduct("Bake House Famous", 25002, 3, 3.29f, 6, 595f,
      ProductType.rollSandwich, Flavor.plain,
      SuperIngredient.enrichedUnbleachedWheatFlour, Ingredient.water,
      SuperIngredient.frenchBase, Ingredient.salt, Ingredient.yeast));

      return d;
    }
  }

  public static Dictionary<int, BakeryProduct> productsByID = CreateDictionary;

  public static ICollection<BakeryProduct> ProductsAvalible = productsByID.Values;

  static private BakeryProduct _selectedProduct;

  public static BakeryProduct FindByIDNumber(int idNumber){

    return productsByID[idNumber];
  }
/*
  private StringBuilder _searchString = new StringBuilder("");

  public List<BakeryProduct> RealtimeTextSearch(char c, bool delete){
    if(!delete){
      _searchString.Append(c);
    }else{
      _searchString.Remove(_searchString.Length - 1, 1);
    }
    return textOnlySearch(_searchString);
  }

  //not used at the moment
  private List<BakeryProduct> enhancedSearch(StringBuilder searchTerm,
  ProductType productType, Flavor flavor, int count, int width, int layers, ){
    List<BakeryProduct> bpl = new List<BakeryProduct>();
    if(searchTerm != ""){

      foreach (BakeryProduct bp in ProductsAvalible){
        if(bp.fullProductName.Contains(searchTerm.ToString()) && bp.IsValidItem){

          if(bp.productType == productType && pb.flavor == flavor){

            if((count < 0 || bp.productType.CheckCountForSearch(count)) && (width < 0 || bp.productType.CheckWidthForSearch(width)) &&
                (layers < 0 || bp.productType.CheckLayersForSearch(layers))){
              bpl.Add(bp);
            }
          }
        }
      }
    }
    return bpl;
  }*/

  static private bool checkNumberForSearch(int number, int numberToCheckAgainst){
    if(number == 0)
      return true;
    return (number == numberToCheckAgainst);
  }

  static private List<BakeryProduct> semiEnhancedSearch(string searchTerm, int count, int width, int layers){
    List<BakeryProduct> bpl = new List<BakeryProduct>();

    foreach (BakeryProduct bp in ProductsAvalible){
      if(bp.fullProductName.Contains(searchTerm) && bp.IsValidItem){

        if (checkNumberForSearch(count, bp.count) && checkNumberForSearch(layers, bp.layers) && checkNumberForSearch(width, bp.width)){

          bpl.Add(bp);
        }
      }
    }
    return bpl;
  }

  static private Status SearchForItem(){

    Console.WriteLine("Enter the name or part the name of the item you want "+
    "to search for. In addition, optional paramaters such as the count, the "+
    "width (for cakes, tarts, and pies), and the number of layers (for cakes) "+
    "can be entered. All paramiters, including any optional ones, should be "+
    "seperated from each other with a colon (:). For example, to search for "+
    "three inch fruit tart, the user would enter \"fruit tart:0:3\". To exit "+
    "from this screen, type (menu), (enter), or (quit).");

    do{
      int count = 0, width = 0, layers = 0;
      string[] search = Console.ReadLine().ToLower().Split(':');

      if(search[0].StartsWith("menu")){
        return Status.Menu;
      }
      if(search[0].StartsWith("enter")){
        return Status.EnterNumber;
      }
      if(search[0].StartsWith("quit")){
        return Status.Quit;
      }

      try{
        count = Int32.Parse(search[1]);
      }
      catch (IndexOutOfRangeException){
        count = 0;
      }
      catch (OverflowException){
        count = 0;
      }
      catch (FormatException){
        count = 0;
      }

      try{
        width = Int32.Parse(search[2]);
      }
      catch (IndexOutOfRangeException){
        width = 0;
      }
      catch (OverflowException){
        width = 0;
      }
      catch (FormatException){
        width = 0;
      }

      try{
        layers = Int32.Parse(search[3]);
      }
      catch (IndexOutOfRangeException){
        layers = 0;
      }
      catch (OverflowException){
        layers = 0;
      }
      catch (FormatException){
        layers = 0;
      }

      List<BakeryProduct> l = semiEnhancedSearch(search[0], count, width, layers);

      foreach (BakeryProduct bp in l){
        Console.WriteLine(string.Format("{0} - {1}", bp.IDNumber, bp.fullProductName));
      }
    }
    while(_status != Status.Searching);
    return _status;
  }

  public enum Status{
    Welcome,
    Menu,
    EnterNumber,
    LableInfo,
    Searching,
    Quit
  };

  static private Status _status = Status.Welcome;

  public delegate Status BakeryCommand();

  static private Status WelcomeScreen(){
    do{
      Console.WriteLine("Welcome to the bakery selection menu. Do you wish to "+
      "(s)earch for a product, show the (m)enu, (e)nter a product ID number, "+
      "or (q)uit?\n");
      //ConsoleKeyInfo k = Console.ReadKey();
      char c = Console.ReadLine().ToLower()[0];

      switch (c) {
        case 's':
        return Status.Searching;
        case 'e':
        return Status.EnterNumber;
        case 'm':
        return Status.Menu;
        case 'q':
        return Status.Quit;
        default:
        Console.WriteLine("Unkown input.\n");
        break;
      }

    }while(_status == Status.Welcome);
    return _status;
  }

  static private Status MenuScreen(){
    Console.WriteLine("ProductMenu:\n");
    foreach (BakeryProduct b in ProductsAvalible)
    {
      Console.WriteLine(string.Format("{0} - {1}\n", b.IDNumber, b.productName));
    }

    do{//productsByID
      Console.WriteLine("Do you want (s)earch for a product, (e)nter a "+
      "product ID number, refresh the (m)enu, or (q)uit?\n");
      //ConsoleKeyInfo k = Console.ReadKey();
      char c = Console.ReadLine().ToLower()[0];

      switch (c) {
        case 's':
        return Status.Searching;
        case 'e':
        return Status.EnterNumber;
        case 'm':
        Console.WriteLine("ProductMenu:\n");
        foreach (BakeryProduct b in ProductsAvalible)
        {
          Console.WriteLine(string.Format("{0} - {1}\n", b.IDNumber, b.productName));
        }
        break;

        case 'q':
        return Status.Quit;
        default:
        Console.WriteLine("Unkown input.\n");
        break;
      }

    }while(_status == Status.Menu);
    return _status;
  }

  static private Status EnterID_Number(){

    do{
      Console.WriteLine("Enter the ID number of your product, or another key "+
      "to (s)earch for a product, show the (m)enu, (e)nter a product ID "+
      "number, or (q)uit. Press enter after entering the number or key.\n");
      string s = Console.ReadLine();
      int i;

      if(s.Length != 5)
      {
        char c = s.ToLower()[0];

        switch (c) {
          case 's':
          return Status.Searching;
          case 'm':
          return Status.Menu;
          case 'e':
          Console.WriteLine("You are already at the enter number screen.\n");
          break;
          default:
          Console.WriteLine("Invalid number or input.");
          break;
        }

      }
      else if (!Int32.TryParse(s, out i))
      {
        Console.WriteLine("Invalid number");
      }
      else if(!productsByID.ContainsKey(i))
      {
        Console.WriteLine("Number not found");
      }
      else
      {
        _selectedProduct = productsByID[i];
        return Status.LableInfo;
      }
    }while(_status == Status.EnterNumber);
    return _status;
  }

  static private Status ShowLableInfo(){

    double packageDays = 0.0, expirationDays = 0.0;

    //ModifyAndPrintExperationDate
    Console.WriteLine(string.Format(_selectedProduct.labelToPrint,
    _selectedProduct.PrintExperationDate));

    Console.WriteLine("To make changes to the packaging and/or expiration "+
    "date, enter the amount of days that you want to add or subtract from "+
    "them, seperating the two with a colon (:). For example, if an item was "+
    "packaged yesterday, then the user would enter \"-1:0\". Conversly, if "+
    "the user wanted to add two days to the experation date, they would "+
    "enter \"0:2\". Or you can return to the (s)earch or (m)enu screen, "+
    "(e)nter a product ID number, or (q)uit. Be warned that endering a letter "+
    "command (s, m, e, or q) will take higher priority over, e.g. if the user "+
    "enters \"m:3\" the program will display the menu screen.");

    do{
      bool notModifiedP = false, notModifiedE = false;
      string[] ip = Console.ReadLine().ToLower().Split(':');

      if(ip.Length < 1)
      {
        Console.WriteLine("Zero-length input detected.");
      }
      else
      {
        try {
          packageDays = Double.Parse(ip[0]);
        }
        catch (OverflowException) {
          notModifiedP = true;
          Console.WriteLine("Entered value excceds the maximum/minimum "+
          "allowed values for double data type.");
        }
        catch (FormatException) {
          notModifiedP = true;
          switch (ip[0][0]) {
            case 's':
            return Status.Searching;
            case 'm':
            return Status.Menu;
            case 'e':
            return Status.EnterNumber;
            case 'q':
            return Status.Quit;
            default:
            Console.WriteLine("Invalid number or input");
            break;
          }
        }

        if (ip.Length >= 2)
        {
          try {
            expirationDays = Double.Parse(ip[1]);
          }
          catch (OverflowException) {
            notModifiedE = true;
            Console.WriteLine("Entered value excceds the maximum/minimum "+
            "allowed values for double data type.");
          }
          catch (FormatException) {
            notModifiedE = true;
            Console.WriteLine("Invalid number or input");
          }
        }

        if (!notModifiedP && !notModifiedE){
          Console.WriteLine(string.Format(_selectedProduct.labelToPrint,
          _selectedProduct.ModifyAndPrintExperationDate(packageDays,
            expirationDays)));
        }
      }
    }while(_status == Status.LableInfo);
    return _status;
  }

  static public Dictionary<Status, BakeryCommand> CommandDict;

  static private Dictionary<Status, BakeryCommand> SetUpCommandDict{
    get{
      Dictionary<Status, BakeryCommand> d = new Dictionary<Status, BakeryCommand>();
      d.Add(Status.Welcome, WelcomeScreen);
      d.Add(Status.Menu, MenuScreen);
      d.Add(Status.EnterNumber, EnterID_Number);
      d.Add(Status.LableInfo, ShowLableInfo);
      d.Add(Status.Searching, SearchForItem);
      return d;
    }
  }

  public static void Main(){
    CommandDict = SetUpCommandDict;
    _status = Status.Welcome;

    while (_status != Status.Quit){

      _status = CommandDict[_status]();
    }
  }
}
