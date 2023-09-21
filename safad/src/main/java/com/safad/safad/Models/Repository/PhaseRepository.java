package com.safad.safad.Models.Repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

import com.safad.safad.Models.Entity.Phase;

public interface PhaseRepository extends JpaRepository<Phase,Long>
{
    @Query("SELECT u FROM Phase u WHERE u.Id_Phase = ?1")
    Phase findById_Phase(Long Id_Phase);
}
