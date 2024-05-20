using BuilderPractice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderLibrary
{
    public static class Dierctor
    {
        public static Car ConstructSuzukiAlto(CarBuilder builder, int kmz = 50)
        {
           var car= builder.ofBrand("Suzuki").OfModel("alto")
                .SetNumberOfSeats(4)
                .SetFeulConsumption(19)
                .ThatDrivedThisKm(kmz).withColor("black").Build();
            return car;
        }
    }
}
