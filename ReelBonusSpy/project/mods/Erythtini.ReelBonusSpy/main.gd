extends Node

func _ready():
	print("ReelBonusSpy added to scene tree. What're you pokin' around in the logs for? >w0")
	
func fishCatch(fish_chance, was_reeling):
	#if (was_reeling): print("you were reeling, i saw it")
	
	var randResult = randf()
	print("[Erythtini.ReelBonusSpy] result: ", randResult, " your fish_chance: ", fish_chance, " x1.1: ", fish_chance*1.1)
	
	# failed at catching
	if randResult > fish_chance:
		#if config[ShowFailures]:
		if (!was_reeling and !(randResult > fish_chance * 1.1)):
			PlayerData._send_notification("The reel bonus would've caught you a fish just now...", 1)
		return true
		
	# succeeded at catching
	else:
		#if config[ShowSuccesses]:
		if (was_reeling and randResult > fish_chance / 1.1 ):
			PlayerData._send_notification("The reel bonus caught you this fish!")
		return false
	
