def moduleMasses = new File("input.txt").readLines()

println(new FuelCalculator(moduleMasses).calculateFuel())

/**
 * Calculator for the necessary fuel
 */
class FuelCalculator {

    private List<Integer> moduleMasses = []

    /**
     * Constructor for the FuelCalculator.
     * @param moduleMasses the masses of all modules. Need to be mapped to integers (see FuelCalculator#moduleMasses)
     */
    FuelCalculator(moduleMasses) {
        this.moduleMasses = moduleMasses.collect { mass -> mass as int }
    }

    /**
     * Calculate the fuel including the mass of the fuel itself.
     * @return the necessary fuel
     */
    def calculateFuel() {
        def necessaryFuel = 0

        moduleMasses.each { mass -> necessaryFuel += calculateFuelForModule(mass) }

        return necessaryFuel
    }

    private def calculateFuelForModule(int moduleMass) {
        def fuelMass = Math.floor(moduleMass / 3) - 2 as int

        return fuelMass <= 0 ? 0 : fuelMass + calculateFuelForModule(fuelMass)
    }
}
