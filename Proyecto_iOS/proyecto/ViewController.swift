


/* ViewController: Se encuentra conectado con el TexBox de la pagina principal 
   Crea y carga la base de datos */

import UIKit

class ViewController: UIViewController {
    
    //Cracion de objeto que corresponde a la DataBase
    var db = NSKeyedUnarchiver.unarchiveObjectWithFile(LocalDatabase.ArchiveURL.path!) as? LocalDatabase;

    //TextBox inicial
    @IBOutlet weak var idUsuario: UITextField!
    
    //Se pinta el fondo y se carga la base de datos
    override func viewDidLoad() {
        super.viewDidLoad()
        
        self.view.backgroundColor = UIColor(patternImage: UIImage (named: "ppal2.png")!)
        
        if db == nil {
            db = LocalDatabase(usuarios: [User]())
            NSKeyedArchiver.archiveRootObject(db!, toFile: LocalDatabase.ArchiveURL.path!)
        }
        
    }

    override func didReceiveMemoryWarning() {
        super.didReceiveMemoryWarning()
    }
    

    //Cuando se da click en el boton enviar, se crea una nueva ventana del storyboard (contactos), se pasa el user id
    @IBAction func iniciarSesion(sender: AnyObject) {
        let vc = storyboard!.instantiateViewControllerWithIdentifier("contactos") as! ContactosViewController
        vc.user = getSelectedUser(idUsuario.text!)
        self.presentViewController(vc, animated: true, completion: nil)

    }
    
    //Se verifica si el usuario ingresado existe en la Database o no, si existe se retorna el usuario, si no se crea 
    func getSelectedUser(id:String) -> User
    {
        let newUser = User(contactos: [Contacto](), id: id)!
        if (db != nil){
            for user in db!.usuarios {
                if (id == user.id) {
                    print("usuario guardado previamente")
                    return user;
                }
            }
            db?.usuarios.append(newUser)
            NSKeyedArchiver.archiveRootObject(db!, toFile: LocalDatabase.ArchiveURL.path!)

        }
        
        return newUser
        
    }
}

