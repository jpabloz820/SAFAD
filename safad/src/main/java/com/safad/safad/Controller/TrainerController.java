package com.safad.safad.Controller;

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

import com.safad.safad.Models.Entity.Phase;
import com.safad.safad.Models.Repository.PhaseRepository;

@Controller
@SessionAttributes("entrenador")

public class TrainerController
{
    @Autowired
    private PhaseRepository phaseRepository;

    @GetMapping("/login_Trainer")

    public String login_Trainer()
    {
        return "Trainers/login_Trainer";
    }
    
    @GetMapping("/Index_Trainer")

    public String Trainers()
    {
        return "Trainers/Index_Trainer";
    }

    @GetMapping("/List_Phase")

    public String list_Phase(Model model)
    {
        model.addAttribute("titulo", "MIS ENTRENOS");
        model.addAttribute("phase", phaseRepository.findAll());

        return "Trainers/List_Phase";
    }

    @GetMapping("/Form_Phase")

    public String crear_Phase(Model model)
    {
        Phase phase = new Phase();

        model.addAttribute("titulo", "CREAR FASE DE ENTRENAMIENTO");
        model.addAttribute("phase", phase);

        return "Trainers/Form_Phase";
    }

    @PostMapping("/Form_Phase")

    public String guardar(@Valid Phase phase, BindingResult Resultado, SessionStatus status, Model model)
    {
        if(Resultado.hasErrors())
        {
            model.addAttribute("titulo", "CREAR FASE DEPORTIVA");
            model.addAttribute("phase", phase);
            
            return "Trainers/Form_Phase";
        }

        phaseRepository.save(phase);
        status.setComplete();

        return "redirect:/List_Phase";
    }

    @GetMapping("/Form_Phase/{Id_Phase}")

    public String editar_phase(@PathVariable(value = "Id_Phase") Long Id_Phase, Model model)
    {
        Phase phase = new Phase();

        if(Id_Phase > 0)
        {
            phase = phaseRepository.findById_Phase(Id_Phase);
        }
        else
        {
            return "redirect:/List_Phase";
        }

        model.addAttribute("Titulo", "EDITAR FASE");
        model.addAttribute("phase", phase);

        return  "Trainers/Form_Phase";
    }

    @GetMapping("/eliminar/{Id_Phase}")

    public String eliminar_phase(@PathVariable Long Id_Phase)
    {
        if(Id_Phase > 0)
        {
            phaseRepository.deleteById(Id_Phase);
        }
        
        return "redirect:/List_Phase";
    }
}