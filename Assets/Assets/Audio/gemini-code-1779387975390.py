import os
import time
import sys

class PlayerCharacter:
    def __init__(self):
        # Physical Stats
        self.hunger = 75  # out of 100
        self.hydration = 80
        
        # Spiritual Stats (Chakras)
        self.root_chakra = 50
        self.heart_chakra = 60
        
        # Quest Progress
        self.main_quest_progress = 35

    def display_ui(self):
        # Clear screen for that clean app/game feel
        os.system('cls' if os.name == 'nt' else 'clear')
        
        print("====================================================")
        print("          CHARACTER MENU: JONATHAN V1.0             ")
        print("====================================================")
        
        # 1. Physical Hub (Survival Bars)
        hunger_bars = int(self.hunger / 10)
        hydr_bars = int(self.hydration / 10)
        
        print("\n[ PHYSICAL STATUS ]")
        print(f"  HUNGER:    [{'█' * hunger_bars}{' ' * (10 - hunger_bars)}] {self.hunger}%")
        print(f"  HYDRATION: [{'█' * hydr_bars}{' ' * (10 - hydr_bars)}] {self.hydration}%")
        if self.hunger < 40:
            print("  ⚠️ STATUS EFFECT: [SILENT HUNGER] - Fuel tank low. Remind yourself to eat!")

        # 2. Spiritual Hub (Chakras)
        print("\n[ ENERGETIC NODES ]")
        print(f"  🔴 Root Chakra  (Grounding): {self.root_chakra}% [Status: {'STABLE' if self.root_chakra >= 50 else 'WEAKENED'}]")
        print(f"  💚 Heart Chakra (Connection): {self.heart_chakra}% [Status: {'STABLE' if self.heart_chakra >= 50 else 'WEAKENED'}]")

        # 3. Quest Log
        quest_bars = int(self.main_quest_progress / 10)
        print("\n[ QUEST LOG ]")
        print(f"  ⚔️ Main Quest: Build Life Dashboard")
        print(f"     Progression: [{'█' * quest_bars}{' ' * (10 - quest_bars)}] {self.main_quest_progress}%")
        
        print("\n====================================================")
        print("  1) Log Meal        2) Log Daily Journal    3) Prayer Box")
        print("  4) Work on Quest   5) Simulate Time Pass   6) Exit")
        print("====================================================")

    def trigger_prayer_box(self):
        os.system('cls' if os.name == 'nt' else 'clear')
        print("=== THE PRAYER BOX ===")
        print("Type your message/worry below to release it from your mental inventory.\n")
        prayer = input("> ")
        
        print("\nProcessing release...")
        time.sleep(1)
        
        # Text dissolving/burning effect simulation
        words = ["*", ".", " ", ""]
        for stage in words:
            os.system('cls' if os.name == 'nt' else 'clear')
            print("=== THE PRAYER BOX ===")
            if stage:
                print(f"\nReleasing: {prayer.replace(prayer, stage * len(prayer))}")
            else:
                print("\n[ Mental Inventory Cleared. ]")
            time.sleep(0.4)
        input("\nPress Enter to return to UI...")

    def update_logs(self):
        os.system('cls' if os.name == 'nt' else 'clear')
        print("=== DAILY JOURNAL LOG ===")
        print("How are you feeling? (Keywords: 'anxious', 'happy', 'tired')")
        entry = input("> ").lower()
        
        if "anxious" in entry or "stressed" in entry:
            self.root_chakra = max(20, self.root_chakra - 25)
            print("\nCalculating stats... Root Chakra has diminished. Grounding required.")
        elif "happy" in entry or "grateful" in entry:
            self.heart_chakra = min(100, self.heart_chakra + 20)
            self.root_chakra = min(100, self.root_chakra + 10)
            print("\nCalculating stats... Energy centers are glowing.")
        else:
            print("\nLog recorded. Stats stabilized.")
        
        time.sleep(2)

def main():
    player = PlayerCharacter()
    
    while True:
        player.display_ui()
        choice = input("\nSelect an action: ")
        
        if choice == "1":
            player.hunger = min(100, player.hunger + 30)
            player.hydration = min(100, player.hydration + 20)
        elif choice == "2":
            player.update_logs()
        elif choice == "3":
            player.trigger_prayer_box()
        elif choice == "4":
            player.main_quest_progress = min(100, player.main_quest_progress + 10)
        elif choice == "5":
            # Simulate natural decay over time
            player.hunger = max(0, player.hunger - 15)
            player.hydration = max(0, player.hydration - 20)
        elif choice == "6":
            print("\nSaving local profile... Goodbye.")
            break

if __name__ == "__main__":
    main()