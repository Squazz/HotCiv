using Microsoft.VisualStudio.TestTools.UnitTesting;
using Squazz.HotCiv.Strategies;

namespace Squazz.HotCiv.Core.Tests
{
    [TestClass]
    public class AlphaCivTests
    {
        Game _game;
        [TestInitialize]
        public void Initialize()
        {
            _game = new Game(new AgeStrategyLinear(), new WinningStrategyRedWins());
        }

        [TestMethod]
        public void ShouldStatInAge4000BC()
        {
            Assert.AreEqual(-4000, _game.Age, "Age should be -4000");
        }

        [TestMethod]
        public void ShouldIncreaseAgeBy100EachRound()
        {
            Assert.AreEqual(-4000, _game.Age, "Age should be -4000");
            EndRounds();
            Assert.AreEqual(-3900, _game.Age, "Age should be -3900");
            EndRounds(2);
            Assert.AreEqual(-3700, _game.Age, "Age should be -3700");
        }

        [TestMethod]
        public void RedShouldBeFirst()
        {
            Assert.AreEqual(Player.RED, _game.PlayerInTurn, "RED should be the first player");
        }

        [TestMethod]
        public void BlueShouldBeSecond()
        {
            _game.EndOfTurn();
            Assert.AreEqual(Player.BLUE, _game.PlayerInTurn, "BLUE should be the secund player");
        }

        [TestMethod]
        public void RedShouldBeAfterBlue()
        {
            Assert.AreEqual(Player.RED, _game.PlayerInTurn, "RED should be first");
            _game.EndOfTurn();
            Assert.AreEqual(Player.BLUE, _game.PlayerInTurn, "BLUE should be second");
            _game.EndOfTurn();
            Assert.AreEqual(Player.RED, _game.PlayerInTurn, "RED should be after BLUE");

        }

        [TestMethod]
        public void BlueShouldBeAfterRed()
        {
            Assert.AreEqual(Player.RED, _game.PlayerInTurn, "RED should be first");
            _game.EndOfTurn();
            Assert.AreEqual(Player.BLUE, _game.PlayerInTurn, "BLUE should be second");
            _game.EndOfTurn();
            Assert.AreEqual(Player.RED, _game.PlayerInTurn, "RED should be after BLUE");
            _game.EndOfTurn();
            Assert.AreEqual(Player.BLUE, _game.PlayerInTurn, "BLUE should be after RED");

        }

        [TestMethod]
        public void ShouldHaveCityAt1_1()
        {
            Assert.IsNotNull(_game.GetCityAt(new Position(1, 1)), "Should have a city at 1,1");
        }

        [TestMethod]
        public void ShouldHaveCityAt4_1()
        {
            Assert.IsNotNull(_game.GetCityAt(new Position(4, 1)), "Should have a city at 4,1");
        }

        [TestMethod]
        public void RedShouldOwnCityAt1_1()
        {
            ICity city = _game.GetCityAt(new Position(1, 1));
            Assert.IsNotNull(city, "Should have a city at 1,1");
            Assert.AreEqual(Player.RED, city.Owner, "RED should own city at 1,1");
        }

        [TestMethod]
        public void BlueShouldOwnCityAt4_1()
        {
            ICity city = _game.GetCityAt(new Position(4, 1));
            Assert.IsNotNull(city, "Should have a city at 4,1");
            Assert.AreEqual(Player.BLUE, city.Owner, "BLUE should own city at 4,1");
        }

        [TestMethod]
        public void ShouldNotHaveCityAt3_0()
        {
            Assert.IsNull(_game.GetCityAt(new Position(3,0)), "There shouldn't be a city at 3,0");
        }

        [TestMethod]
        public void SouldStartWithArcherAt2_0()
        {
            Position position = new Position(2, 0);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "We should have a unit at 2,0");
            Assert.AreEqual(GameConstants.Archer, unit.Type, "Type should be archer");
        }

        [TestMethod]
        public void SouldStartWithRedArcherAt2_0()
        {
            Position position = new Position(2, 0);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "We Should have a unit at 2,0");
            Assert.AreEqual(GameConstants.Archer, unit.Type, "Type should be archer");
            Assert.AreEqual(Player.RED, unit.Owner, "Owner should be RED");
        }

        [TestMethod]
        public void ShouldStartWithLegionAt3_2()
        {
            Position position = new Position(3, 2);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "We should have a unit at 3,2");
            Assert.AreEqual("legion", unit.Type, "Type should be legion");
        }

        [TestMethod]
        public void ShouldStartWithBlueLegionAt3_2()
        {
            Position position = new Position(3, 2);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "We should have a unit at 3,2");
            Assert.AreEqual("legion", unit.Type, "Type should be legion");
            Assert.AreEqual(Player.BLUE, unit.Owner, "Owner should be BLUE");
        }

        [TestMethod]
        public void ShouldStartWithSettlerAt4_3()
        {
            Position position = new Position(4,3);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "Should have a unit at 4,3");
            Assert.AreEqual("settler", unit.Type, "Type should be settler");
        }

        [TestMethod]
        public void ShouldStartWithRedSettlerAt4_3()
        {
            Position position = new Position(4, 3);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "Should have a unit at 4,3");
            Assert.AreEqual("settler", unit.Type, "Type should be settler");
            Assert.AreEqual(Player.RED, unit.Owner, "Owner should be RED");
        }

        [TestMethod]
        public void ArchersShouldHave2Attack()
        {
            Position position = new Position(2,0);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "Should have a unit at 0,0");
            Assert.AreEqual(GameConstants.Archer, unit.Type, "Type should be archer");
            Assert.AreEqual(2, unit.Attack);
        }

        [TestMethod]
        public void ArchersShouldHave3Defence()
        {
            Position position = new Position(2, 0);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "Should have a unit at 2,0");
            Assert.AreEqual(GameConstants.Archer, unit.Type, "Type should be archer");
            Assert.AreEqual(3, unit.Defense);
        }

        [TestMethod]
        public void LegionsShouldHave4Attack()
        {
            Position position = new Position(3, 2);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "Should have a unit at 3,2");
            Assert.AreEqual("legion", unit.Type, "Type should be legion");
            Assert.AreEqual(4, unit.Attack);
        }

        [TestMethod]
        public void LegionsShouldHave2Defence()
        {
            Position position = new Position(3, 2);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "Should have a unit at 3,2");
            Assert.AreEqual("legion", unit.Type, "Type should be legion");
            Assert.AreEqual(2, unit.Defense);
        }

        [TestMethod]
        public void SettlersShouldHave0Attack()
        {
            Position position = new Position(4, 3);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "Should have a unit at 4,3");
            Assert.AreEqual("settler", unit.Type, "Type should be settler");
            Assert.AreEqual(0, unit.Attack);
        }

        [TestMethod]
        public void SettlersShouldHave3Defence()
        {
            Position position = new Position(4, 3);
            IUnit unit = _game.GetUnitAt(position);
            Assert.IsNotNull(unit, "Should have a unit at 4,3");
            Assert.AreEqual("settler", unit.Type, "Type should be settler");
            Assert.AreEqual(3, unit.Defense);
        }

        [TestMethod]
        public void ShouldNotHaveAWinnerBefore3000BC()
        {
            Assert.IsNull(_game.GetWinner(), "We shouldn't have a winner in 4000BC");
            EndRounds(9);
            Assert.IsNull(_game.GetWinner(), "We shouldn't have a winner in 3100BC");
        }

        [TestMethod]
        public void RedShouldWinInYear3000BC()
        {
            EndRounds(10);
            Assert.IsNotNull(_game.GetWinner(), "We should have a winner in 3000BC");
        }

        [TestMethod]
        public void CitiesShouldStartWithPopulationOf1()
        {
            ICity redCity = _game.GetCityAt(new Position(1,1));
            ICity blueCity = _game.GetCityAt(new Position(4, 1));

            Assert.AreEqual(1, redCity.Size, "RED city should have a size of 1");
            Assert.AreEqual(1, blueCity.Size, "BLUE city should have a size of 1");
        }

        [TestMethod]
        public void CitiesShouldAlwaysHaveaPopulationOf1()
        {
            ICity redCity = _game.GetCityAt(new Position(1, 1));
            ICity blueCity = _game.GetCityAt(new Position(4, 1));

            Assert.AreEqual(1, redCity.Size, "RED city should have a size of 1");
            Assert.AreEqual(1, blueCity.Size, "BLUE city should have a size of 1");
            EndRounds(10); // End the game
            Assert.AreEqual(1, redCity.Size, "RED city should have a size of 1 in the endgame");
            Assert.AreEqual(1, blueCity.Size, "BLUE city should have a size of 1 in the endgame");
        }

        [TestMethod]
        public void ShouldbeAbleToMoveArcherTo3_0()
        {
            Position from = new Position(2, 0);
            Position to = new Position(3, 0);
            Assert.IsTrue(_game.MoveUnit(from, to), "We should be able to move the unit");
            Assert.IsNotNull(_game.GetUnitAt(to), "Unit should be at the new position");
            Assert.IsNull(_game.GetUnitAt(from), "The unit should not be at the old position");
        }

        [TestMethod]
        public void OnlyOneUnitAtATime()
        {
            Assert.IsTrue(_game.MoveUnit(new Position(2, 0), new Position(3, 1)), "Should be able to make the archers first move");
            Assert.IsTrue(_game.MoveUnit(new Position(4, 3), new Position(4, 2)), "Should be able to make the settlers first move");
            EndRounds();
            Assert.IsFalse(_game.MoveUnit(new Position(3, 1), new Position(4, 2)), "Shouldn't be able to make the archers second move");
        }

        [TestMethod]
        public void ShouldNotBeAbleToExecuteMoveActionIfNoUnitAtOrigin()
        {
            Assert.IsFalse(_game.MoveUnit(new Position(0,0), new Position(0,1)));
        }

        [TestMethod]
        public void ShouldHaveOceanAt1_0()
        {
            ITile tile = _game.GetTileAt(new Position(1, 0));
            Assert.IsNotNull(tile, "There should be a tile at 1,0");
            Assert.AreEqual(GameConstants.Ocean, tile.Type, "Tile should be ocean");
        }

        [TestMethod]
        public void ShouldHaveHillsAt0_1()
        {
            ITile tile = _game.GetTileAt(new Position(0, 1));
            Assert.IsNotNull(tile, "There should be a tile at 0,1");
            Assert.AreEqual(GameConstants.Hills, tile.Type, "Tile should be hills");
        }

        [TestMethod]
        public void ShouldHaveMountainsAt2_2()
        {
            ITile tile = _game.GetTileAt(new Position(2, 2));
            Assert.IsNotNull(tile, "There should be a tile at 2,2");
            Assert.AreEqual(GameConstants.Mountains, tile.Type, "Tile should be hills");
        }

        [TestMethod]
        public void ShouldHavePlainIfNothingElse()
        {
            Position ocean = new Position(0, 1);
            Position hills = new Position(1, 0);
            Position mountains = new Position(2, 2);
            for (int i = 0; i <= 15; i++)
            {
                for (int j = 0; j <= 15; j++)
                {
                    Position position = new Position(i, j);
                    if (!Equals(position, ocean) && !Equals(position, hills) && !Equals(position, mountains))
                    {
                        ITile tile = _game.GetTileAt(position);
                        Assert.IsNotNull(tile, "There should be a tile at " + i + "," + j);
                        Assert.AreEqual(GameConstants.Plains, tile.Type, "Tile should be plains");
                    }
                }
            }
        }

        [TestMethod]
        public void RedShouldBeAbleToMoveOwnUnits()
        {
            Assert.IsTrue(_game.MoveUnit(new Position(2, 0), new Position(3, 1)), "Should be able to move the red archer");
        }

        [TestMethod]
        public void BlueShouldBeAbleToMoveOwnUnits()
        {
            _game.EndOfTurn();
            Assert.IsTrue(_game.MoveUnit(new Position(3, 2), new Position(3, 1)), "Should be able to move the legion");
        }

        [TestMethod]
        public void RedShouldNotBeAbleToMoveBluesUnits()
        {
            _game.EndOfTurn();
            Assert.IsFalse(_game.MoveUnit(new Position(2, 0), new Position(3, 1)), "Should be able to move the red archer");
        }

        [TestMethod]
        public void BlueShouldNotBeAbleToMoveRedsUnits()
        {
            Assert.IsFalse(_game.MoveUnit(new Position(3, 2), new Position(3, 1)), "Should be able to move the legion");
        }

        [TestMethod]
        public void ArcherWinsIfItAttacksTheLegion()
        {
            _game.EndOfTurn();
            Assert.IsTrue(_game.MoveUnit(new Position(3, 2), new Position(3, 1)), "Should be able to move the legion");
            Assert.AreEqual(_game.GetUnitAt(new Position(3, 1)).Type, GameConstants.Legion, "The unit at 3,1 should now be the legion");
            _game.EndOfTurn();
            Assert.IsTrue(_game.MoveUnit(new Position(2, 0), new Position(3, 1)), "Should be able to attack with the archer");
            Assert.AreEqual(_game.GetUnitAt(new Position(3,1)).Type,GameConstants.Archer, "The unit at 3,1 should now be the archer");
        }

        [TestMethod]
        public void LegionWinsIfItAttacksTheArcher()
        {
            Assert.IsTrue(_game.MoveUnit(new Position(2, 0), new Position(3, 1)), "Should be able to attack with the archer");
            Assert.AreEqual(_game.GetUnitAt(new Position(3, 1)).Type, GameConstants.Archer, "The unit at 3,1 should now be the archer");
            _game.EndOfTurn();
            Assert.IsTrue(_game.MoveUnit(new Position(3, 2), new Position(3, 1)), "Should be able to move the legion");
            Assert.AreEqual(_game.GetUnitAt(new Position(3, 1)).Type, GameConstants.Legion, "The unit at 3,1 should now be the legion");
        }

        [TestMethod]
        public void CitiesStartWith0ProdtionAccumulated()
        {
            Assert.AreEqual(0,_game.GetCityAt(new Position(1, 1)).Vault, "REDs city should start with 0 production accumulated");
        }

        [TestMethod]
        public void CitiesProduces6ProductionEachRound()
        {
            Position redCityPosition = new Position(1, 1);
            ICity redCity = _game.GetCityAt(redCityPosition);
            Position blueCityPosition = new Position(4, 1);
            ICity blueCity = _game.GetCityAt(blueCityPosition);
            Assert.AreEqual(0, redCity.Vault, "REDs city should start with 0 production accumulated");
            Assert.AreEqual(0, blueCity.Vault, "BLUEs city should start with 0 production accumulated");
            EndRounds();
            Assert.AreEqual(6, redCity.Vault, "REDs city should now have 6 production accumulated");
            Assert.AreEqual(6, blueCity.Vault, "BLUEs city should now have 6 production accumulated");
            EndRounds(2);
            Assert.AreEqual(18, redCity.Vault, "REDs city should now have 18 production accumulated");
            Assert.AreEqual(18, blueCity.Vault, "BLUEs city should now have 18 production accumulated");
        }

        [TestMethod]
        public void UnitsCannotMoveOurMountains()
        {
            _game.EndOfTurn();
            Assert.IsFalse(_game.MoveUnit(new Position(3,2), new Position(2,2)), "We shouldn't be able to move units over mountains");
        }

        [TestMethod]
        public void UnitsCannotMoveOurOcean()
        {
            Assert.IsFalse(_game.MoveUnit(new Position(2, 0), new Position(1, 0)), "We shouldn't be able to move units over ocean");
        }

        [TestMethod]
        public void RedShouldBeAbleToProduceAnArcher()
        {
            Position redCityPosition = new Position(1, 1);
            ICity redCity = _game.GetCityAt(redCityPosition);
            EndRounds(2);
            Assert.AreEqual(12, redCity.Vault, "Our city should have enough production to create a unit");
            _game.ChangeProductionInCityAt(redCityPosition, GameConstants.Archer);
            EndRounds();
            Assert.IsNotNull(_game.GetUnitAt(redCityPosition), "We should now have produced a unit");
            Assert.AreEqual(GameConstants.Archer, _game.GetUnitAt(redCityPosition).Type, "The type of our newly produces unit should be archer");
        }

        [TestMethod]
        public void RedProducingAUnitShouldSubsrtractTheCostFromTheVault()
        {
            RedShouldBeAbleToProduceAnArcher();
            Position redCityPosition = new Position(1, 1);
            ICity redCity = _game.GetCityAt(redCityPosition);
            Assert.AreEqual(8, redCity.Vault, "Our city should now have 8 production");
        }

        [TestMethod]
        public void BlueShouldBeAbleToProduceAnArcher()
        {
            Position blueCityPosition = new Position(4, 1);
            ICity blueCity = _game.GetCityAt(blueCityPosition);
            EndRounds(2);
            Assert.AreEqual(12, blueCity.Vault, "Our city should have enough production to create a unit");
            _game.ChangeProductionInCityAt(blueCityPosition, GameConstants.Archer);
            EndRounds();
            Assert.IsNotNull(_game.GetUnitAt(blueCityPosition), "We should now have produced a unit");
            Assert.AreEqual(GameConstants.Archer, _game.GetUnitAt(blueCityPosition).Type, "The type of our newly produces unit should be archer");
        }

        [TestMethod]
        public void BlueProducingAUnitShouldSubsrtractTheCostFromTheVault()
        {
            BlueShouldBeAbleToProduceAnArcher();
            Position blueCityPosition = new Position(4, 1);
            ICity blueCity = _game.GetCityAt(blueCityPosition);
            Assert.AreEqual(8, blueCity.Vault, "Our city should now have 8 production");
        }

        [TestMethod]
        public void ShouldResetproductionAfterProducingAUnit()
        {
            Position cityPosition = new Position(1, 1);
            ICity city = _game.GetCityAt(cityPosition);
            EndRounds(2);

            Assert.AreEqual(12, city.Vault, "Our city should have enough production to create a unit");
            _game.ChangeProductionInCityAt(cityPosition, GameConstants.Archer);
            EndRounds();

            Assert.IsNotNull(_game.GetUnitAt(cityPosition), "We should now have produced a unit");
            Assert.AreEqual(GameConstants.Archer, _game.GetUnitAt(cityPosition).Type, "The type of our newly produces unit should be archer");
            Assert.AreEqual(null, city.Production, "The city should not produce anything anymore");
        }

        [TestMethod]
        public void PlaceProducedUnitToTheNorthIfThereIsAUnitInTheRedCity() 
        {
            Position cityPosition = new Position(1, 1);
            ICity city = _game.GetCityAt(cityPosition);
            EndRounds(2);

            Assert.AreEqual(12, city.Vault, "Our city should have enough production to create a unit");
            _game.ChangeProductionInCityAt(cityPosition, GameConstants.Archer);
            EndRounds();

            Assert.IsNotNull(_game.GetUnitAt(cityPosition), "We should now have produced a unit");
            Assert.AreEqual(GameConstants.Archer, _game.GetUnitAt(cityPosition).Type, "The type of our newly produces unit should be archer");
            Assert.AreEqual(8, city.Vault, "Our city should now have 8 production");
            EndRounds();

            Assert.AreEqual(14, city.Vault, "Our city should now have 14 production");
            _game.ChangeProductionInCityAt(cityPosition, GameConstants.Archer);
            EndRounds();

            Position newPosition = new Position(0, 1);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should now have placed the unit to the north");
            Assert.AreEqual(GameConstants.Archer, _game.GetUnitAt(newPosition).Type, "The type of our newly produces unit should be archer");
        }

        [TestMethod]
        public void PlaceProducedUnitToTheNorthIfThereIsAUnitInTheBlueCity()
        {
            Position cityPosition = new Position(4, 1);
            ICity city = _game.GetCityAt(cityPosition);
            EndRounds(2);

            Assert.AreEqual(12, city.Vault, "Our city should have enough production to create a unit");
            _game.ChangeProductionInCityAt(cityPosition, GameConstants.Archer);
            EndRounds();

            Assert.IsNotNull(_game.GetUnitAt(cityPosition), "We should now have produced a unit");
            Assert.AreEqual(GameConstants.Archer, _game.GetUnitAt(cityPosition).Type, "The type of our newly produces unit should be archer");
            Assert.AreEqual(8, city.Vault, "Our city should now have 8 production");
            EndRounds();

            Assert.AreEqual(14, city.Vault, "Our city should now have 14 production");
            _game.ChangeProductionInCityAt(cityPosition, GameConstants.Archer);
            EndRounds();

            Position newPosition = new Position(3, 1);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should now have placed the unit to the north");
            Assert.AreEqual(GameConstants.Archer, _game.GetUnitAt(newPosition).Type, "The type of our newly produces unit should be archer");
        }

        [TestMethod]
        public void AlwaysPlaceProducedUnitCorrectly()
        {
            Position redCityPosition = new Position(1, 1);
            Position blueCityPosition = new Position(4, 1);
            Assert.IsNotNull(_game.GetUnitAt(new Position(2, 0)), "We should have a unit here from the start");
            EndRounds(2);

            // Start producing all of the units
            _game.ChangeProductionInCityAt(redCityPosition, GameConstants.Archer);
            _game.ChangeProductionInCityAt(blueCityPosition, GameConstants.Archer);
            EndRounds(2);

            _game.ChangeProductionInCityAt(redCityPosition, GameConstants.Archer);
            _game.ChangeProductionInCityAt(blueCityPosition, GameConstants.Archer);
            EndRounds(2);

            _game.ChangeProductionInCityAt(redCityPosition, GameConstants.Archer);
            _game.ChangeProductionInCityAt(blueCityPosition, GameConstants.Archer);
            EndRounds(2);

            _game.ChangeProductionInCityAt(redCityPosition, GameConstants.Archer);
            _game.ChangeProductionInCityAt(blueCityPosition, GameConstants.Archer);
            EndRounds(2);

            _game.ChangeProductionInCityAt(redCityPosition, GameConstants.Archer);
            _game.ChangeProductionInCityAt(blueCityPosition, GameConstants.Archer);
            EndRounds();

            _game.ChangeProductionInCityAt(redCityPosition, GameConstants.Archer);
            _game.ChangeProductionInCityAt(blueCityPosition, GameConstants.Archer);
            EndRounds();

            _game.ChangeProductionInCityAt(blueCityPosition, GameConstants.Archer);
            EndRounds(2);

            _game.ChangeProductionInCityAt(blueCityPosition, GameConstants.Archer);
            EndRounds();


            // Test the red units
            Position newPosition = new Position(0, 0);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should now have produced a unit");
            newPosition = new Position(0, 1);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should now have produced a unit");
            newPosition = new Position(0, 2);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should now have produced a unit");
            newPosition = new Position(1, 0);
            Assert.IsNull(_game.GetUnitAt(newPosition), "We should not have produced a unit in the ocean");
            newPosition = new Position(1, 1);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should now have produced a unit");
            newPosition = new Position(1, 2);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should now have produced a unit");
            newPosition = new Position(2, 0);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should have a unit here from the start");
            newPosition = new Position(2, 1);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should now have produced a unit");
            newPosition = new Position(2, 2);
            Assert.IsNull(_game.GetUnitAt(newPosition), "We should not have produced a unit on a mountain");

            // Test the blue units
            newPosition = new Position(3, 0);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should now have produced a unit");
            newPosition = new Position(3, 1);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should now have produced a unit");
            newPosition = new Position(3, 2);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should have a unit here from the start");
            newPosition = new Position(4, 0);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should now have produced a unit");
            newPosition = new Position(4, 1);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should now have produced a unit");
            newPosition = new Position(4, 2);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should now have produced a unit");
            newPosition = new Position(5, 0);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should have a unit here from the start");
            newPosition = new Position(5, 1);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should now have produced a unit");
            newPosition = new Position(5, 2);
            Assert.IsNotNull(_game.GetUnitAt(newPosition), "We should now have produced a unit");
            
        }

        [TestMethod]
        public void UnitsShouldOnlyBeAbleToMove1StepEachRound()
        {
            Assert.IsFalse(_game.MoveUnit(new Position(2, 0), new Position(4, 0)));
            Assert.IsTrue(_game.MoveUnit(new Position(2, 0), new Position(3, 0)));
            Assert.IsFalse(_game.MoveUnit(new Position(3, 0), new Position(4, 0)));
            EndRounds();
            Assert.IsTrue(_game.MoveUnit(new Position(3, 0), new Position(4, 0)));
            EndRounds();
            Assert.IsFalse(_game.MoveUnit(new Position(4, 0), new Position(2, 0)));
            Assert.IsTrue(_game.MoveUnit(new Position(4, 0), new Position(3, 0)));
            Assert.IsFalse(_game.MoveUnit(new Position(3, 0), new Position(2, 0)));
            EndRounds();
            Assert.IsTrue(_game.MoveUnit(new Position(3, 0), new Position(2, 0)));
        }

        // Shortcode for ending 2 turns
        private void EndRounds(int rounds = 1)
        {
            for (int i = 1; i <= rounds; i++)
            {
                _game.EndOfTurn();
                _game.EndOfTurn();
            }
        }
    }
}