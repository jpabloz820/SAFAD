package com.safad.safad.Controller;

import javax.servlet.http.HttpSession;
import javax.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.SessionAttributes;
import org.springframework.web.bind.support.SessionStatus;
import com.safad.safad.Models.Entity.People;
import com.safad.safad.Models.Entity.Role;
import com.safad.safad.Models.Repository.PeopleRepository;
import com.safad.safad.Models.Repository.RoleRepository;

@Controller
@SessionAttributes("Deportista")
public class AthleteController 
{
    @Autowired
    private PeopleRepository Aux;
    @Autowired
    private RoleRepository Aux1;
    
    @GetMapping("/Index_Athlete")

    public String Athelete()
    {
        return "Athlete/Index_Athlete";
    }

    @GetMapping("/Datos_Athlete")

    public String Datos(HttpSession session, Model model)
    {
        Long people_Id = (Long) session.getAttribute("Id_People");
        People people = Aux.findById_People(people_Id);

        model.addAttribute("titulo", "Mis Datos");

        if(people != null)
        {
            model.addAttribute("People", people);

            return "Athlete/Datos_Athlete";
        }

        return "Index/Login";
    }

    // @GetMapping("/Listar_Plantel")

    // public String listar_Plantel(Model model)
    // {
    //     model.addAttribute("titulo", "MI TITULAR");
    //     model.addAttribute("plantel", Aux.findAll());

    //     return "Directors/Listar_Plantel";
    // }

    // @GetMapping("/Form_Plantel")

    // public String crear(Model model)
    // {
    //     People people = new People();

    //     model.addAttribute("titulo", "FORMULARIO DEL PLANTEL");
    //     model.addAttribute("people", people);

    //     return "Directors/Form_Plantel";
    // }

    // @PostMapping("/Form_Plantel")

    // public String guardar(@Valid People people, BindingResult Resultado, SessionStatus status, Model model)
    // {
    //     if(Resultado.hasErrors())
    //     {
    //         model.addAttribute("titulo", "FORMULARIO DEL PLANTEL");
    //         model.addAttribute("people", people);
            
    //         return "Directors/Form_Plantel";
    //     }

    //     Aux.save(people);
    //     status.setComplete();

    //     return "redirect:Directors/Listar_Plantel";
    // }

    // @GetMapping("/Form_Plantel/{Id_People}")

    // public String editar(@PathVariable(value = "Id_People") Long Id_People, Model model)
    // {
    //     People people = new People();

    //     if(Id_People > 0)
    //     {
    //         people = Aux.findById_People(Id_People);
    //     }
    //     else
    //     {
    //         return "redirect:Directors/Listar_Plantel";
    //     }

    //     model.addAttribute("Titulo", "Editar usuario");
    //     model.addAttribute("people", people);

    //     return  "Directors/Form_Plantel";
    // }

    // @GetMapping("/Eliminar/{Id_People}")

    // public String Eliminar(@PathVariable Long Id_People)
    // {
    //     if(Id_People > 0)
    //     {
    //         Aux.deleteById(Id_People);
    //     }
        
    //     return "redirect:Directors/Listar_Plantel";
    // }

    // @GetMapping("/List_Role")

    // public String listar_Roles(Model model)
    // {
    //     model.addAttribute("titulo", "ROLES");
    //     model.addAttribute("roles", Aux1.findAll());

    //     return "Directors/List_Role";
    // }

    // @GetMapping("/Form_Role")

    // public String crear_Role(Model model)
    // {
    //     Role role = new Role();

    //     model.addAttribute("role", role);

    //     return "Directors/List_Role";
    // }

    // @PostMapping("/Form_Role")

    // public String guardar_Role(@Valid Role role, BindingResult Resultado, SessionStatus status, Model model)
    // {
    //     if(Resultado.hasErrors())
    //     {
    //         model.addAttribute("role", role);
            
    //         return "Directors/List_Role";
    //     }

    //     Aux1.save(role);
    //     status.setComplete();

    //     return "redirect:List_Role";
    // }

    @GetMapping("/Logout_Athlete")
    public String logout_Athelete(HttpSession session) 
    {
        session.invalidate(); // Invalidar la sesión actual
        return "redirect:/Login";
    }
}
