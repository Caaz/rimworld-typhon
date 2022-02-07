# Typhon
<!-- Add badges from here since it's easy https://github.com/Ileriayo/markdown-badges -->
![Build Status](https://img.shields.io/github/workflow/status/Caaz/rimworld-typhon/Build?style=for-the-badge)![Download](https://img.shields.io/github/downloads-pre/Caaz/rimworld-typhon/latest/total?style=for-the-badge)

[![Discord](https://img.shields.io/discord/259685048914149378?color=%237289DA&label=Discord&logo=discord&logoColor=white&style=for-the-badge)](https://discord.gg/aE6ABXNqPj)    
<!-- ![Steam](https://img.shields.io/badge/steam-%23000000.svg?style=for-the-badge&logo=steam&logoColor=white) -->

## Description

This is a Rimworld Mod that adds the [Typhon aliens from the game Prey](https://prey.fandom.com/wiki/Typhon). Being psychic creatures feels like a perfect addtion to the lore of Rimworld. 

## Testing
If you'd like to test the latest cutting edge version of the mod, checkout the latest release! The *soon to come* steam page will have the latest stable release.

## Planning

The following is the current planned phases and feature sets within them. This isn't set in stone, but it should give a bit of direction, if you'd like to help contribute!

### Phase 1 - Mimic

- [x] Add the [Mimic Typhon](https://prey.fandom.com/wiki/Mimic).
    - [ ] Mimics should have custom sounds!
        - Going to require me to go record some samples to know what they even sound like.
    - [x] Mimics should feed on corpses, multiplying into more mimics!
    - [x] Mimics should copy the visual look of objects, when not in combat. They wait there til they can ambush!
        - [x] They copy features too!
        - [x] There should be a limit on the size of what they copy. 
        - [x] We shouldn't copy connectable buildings, like fences.
        - [x] Shouldn't copy conduits either (fixed via only copying things at building altitude layer.)
- [x] Add Typhon Organs
    - All Typhons will probably drop this, scaled to how big they are. Mimics should only drop a few
- [x] Add Recipes for Skilltrainers
    - Probably could use some balancing
- [ ] Add Recipes for Psytrainers
    - Probably has to be done dynamically, to support modded abilities.
    - [ ] Add psychic abilities from Prey
        - Requires research. Time to actually play Prey myself.
        - This may require this feature to be Royalty dependant! (At least, for psytrainers)
        - Probably going to have to play more Prey to unlock the alien abilities. Should this be a post-release feature?
- [x] Add an event, introducing mimics to an area, using something like a crashed drop pod, that could introduce a single mimic.
    - Maybe with a downed pawn to go with it?
        - Turns out, not required to be interesting.

### Phase 2 - Weaver


- [ ] Add the [Weaver Typhon](https://prey.fandom.com/wiki/Weaver).
    - [x] Assets
    - [ ] Sounds
- [ ] Add a new job to Mimics, to count nearby mimics, and if 6 or more exist, they should fight another mimic, and force it to turn into a Weaver!
- [ ] Add [Coral](https://prey.fandom.com/wiki/Coral)
    - [ ] Seems like this should inflict a fear response in anything that damages it.
    - [ ] Probably should go away with time.
- [ ] Add [Phantoms](https://prey.fandom.com/wiki/Phantom)

## Credits

### Caaz
- Coding, XML, etc.
### Magenta_Ivy
- Art for Mimics and Weavers.
