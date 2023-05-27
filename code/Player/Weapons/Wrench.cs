﻿using Sandbox;

namespace TFS2;

[Library( "tf_weapon_wrench" )]
public class Wrench : TFMeleeBase 
{
	const string BUILDING_HIT_SUCCESS = "weapon_wrench.hit.building.success";
	const string BUILDING_HIT_FAIL = "weapon_wrench.hit.building.fail";
	bool wasSuccess;
	public override void OnHitEntity( Entity entity, TraceResult tr )
	{
		if(entity is TFBuilding building && building.Owner == Owner )
		{
			int usedMetal = 25;
			if ( TFGameRules.Current.IsInSetup )
				usedMetal *= 2;

			usedMetal = TFOwner.ConsumeMetal( usedMetal );
			usedMetal = building.ApplyUpgradeMetal( usedMetal );
			if ( usedMetal > 0 )
				wasSuccess = true;
			else
				wasSuccess = false;
		}

		base.OnHitEntity( entity, tr );
	}

	public override void PlayImpactSound( Entity entity )
	{
		if(entity is TFBuilding building && building.Owner == Owner)
		{
			if ( wasSuccess )
				PlaySound( BUILDING_HIT_SUCCESS );
			else
				PlaySound( BUILDING_HIT_FAIL );

			return;
		}
		else
			base.PlayImpactSound( entity );
	}
}
