package com.safad.safad.Controller;

import java.util.List;

import javax.servlet.http.HttpSession;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;

import com.safad.safad.Models.Entity.People;
import com.safad.safad.Models.Entity.Role;
import com.safad.safad.Models.Repository.PeopleRepository;
import com.safad.safad.Models.Repository.RoleRepository;
import com.safad.safad.Models.Service.PeopleDetails;

@Controller

public class HomeController
{
    @Autowired
    private PeopleDetails peopleDetails;
    @Autowired
    PeopleRepository peopleRepository;
    @Autowired
    private RoleRepository roleRepository;

    @Autowired
    
    public HomeController(PeopleDetails peopleDetails)
    {
        this.peopleDetails = peopleDetails;
    }

    @GetMapping("/Base")

    public String Base()
    {
        return "Plantilla/Base";
    }

    @GetMapping("/")

    public String Home()
    {
        return "Index/Home";
    }

    @GetMapping("/Login")

    public String Login(Model model)
    {
        model.addAttribute("titulo", "Iniciar Sesión");
        return "Index/Login";
    }

    @PostMapping("/Login")

    public String Login(@RequestParam("Email") String Email, @RequestParam("Password") String Password, Model model, HttpSession session)
    {
        People people = peopleRepository.findByEmail(Email);

        if(peopleDetails.Login(Email, Password))
        {
            session.setAttribute("Id_People", people.getId_People());
            
            List<Role> roles = peopleDetails.GetRoles(Email);

            if(roles.get(0).equals(roleRepository.findByName_Role("Entrenador")))
            {
                return "redirect:Index_Trainer";
            }

            if(roles.get(0).equals(roleRepository.findByName_Role("Deportista")))
            {

                return "redirect:Index_Athlete";
            }

            if(roles.get(0).equals(roleRepository.findByName_Role("Profesional")))
            {

                return "redirect:Index_Professional";
            }

            if(roles.get(0).equals(roleRepository.findByName_Role("Familiar")))
            {

                return "redirect:Index_Families";
            }

            if(roles.get(0).equals(roleRepository.findByName_Role("Directivo")))
            {

                return "redirect:Index_Director";
            }

            return "Index/Login";
        }

        model.addAttribute("Error", "Usuario o password incorrecto");

        return "Index/Login";
    }
}
