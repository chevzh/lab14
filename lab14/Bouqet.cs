using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab14
{
    [Serializable]
    public class Bouqet : IEnumerable<Flower>
    {
        protected List<Flower> _flowers;

        public virtual void Add(Flower flower)
        {
            _flowers.Add(flower);
        }

        public Bouqet(IEnumerable<Flower> flowers)
        {
            _flowers = new List<Flower>(flowers);
        }

        public Bouqet()
        {
            _flowers = new List<Flower>();
        }

        IEnumerator<Flower> IEnumerable<Flower>.GetEnumerator()
        {
            return _flowers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _flowers.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join("\n---------------\n" , this);
        }

    }
}
