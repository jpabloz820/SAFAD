package com.safad.safad.Models.Entity;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;
import javax.validation.constraints.NotEmpty;
import javax.validation.constraints.Size;

@Entity
@Table(name = "phase")

public class Phase
{
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long Id_Phase;

    @NotEmpty (message = "Nombre invalido o vacío")
    @Size(min = 5, max = 200)
    public String Description;

    @NotEmpty (message = "Nombre invalido o vacío")
    @Size(min = 5, max = 200)
    public String Achievement_Indicador;

    public String Start_Date;

    public String End_Date;

    public Phase() {
    }

    public Phase(Long id_Phase,
            @NotEmpty(message = "Nombre invalido o vacío") @Size(min = 5, max = 200) String description,
            @NotEmpty(message = "Nombre invalido o vacío") @Size(min = 5, max = 200) String achievement_Indicador,
            String start_Date, String end_Date) {
        Id_Phase = id_Phase;
        Description = description;
        Achievement_Indicador = achievement_Indicador;
        Start_Date = start_Date;
        End_Date = end_Date;
    }

    public Phase(@NotEmpty(message = "Nombre invalido o vacío") @Size(min = 5, max = 200) String description,
            @NotEmpty(message = "Nombre invalido o vacío") @Size(min = 5, max = 200) String achievement_Indicador,
            String start_Date, String end_Date) {
        Description = description;
        Achievement_Indicador = achievement_Indicador;
        Start_Date = start_Date;
        End_Date = end_Date;
    }

    public Long getId_Phase() {
        return Id_Phase;
    }

    public void setId_Phase(Long id_Phase) {
        Id_Phase = id_Phase;
    }

    public String getDescription() {
        return Description;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public String getAchievement_Indicador() {
        return Achievement_Indicador;
    }

    public void setAchievement_Indicador(String achievement_Indicador) {
        Achievement_Indicador = achievement_Indicador;
    }

    public String getStart_Date() {
        return Start_Date;
    }

    public void setStart_Date(String start_Date) {
        Start_Date = start_Date;
    }

    public String getEnd_Date() {
        return End_Date;
    }

    public void setEnd_Date(String end_Date) {
        End_Date = end_Date;
    }
}
