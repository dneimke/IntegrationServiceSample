using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.IntegrationService.Mappings
{
    public abstract class MappingFunction<TIn, TOut>
    {
        private Func<TIn, TOut> _mapper;
        public Func<TIn, TOut> Mapper
        {
            private get
            {
                if (this._mapper == null)
                    return this.Default;

                return _mapper;
            }
            set
            {
                this._mapper = value;
            }
        }

        public TOut Map(TIn input)
        {
            return Mapper(input);
        }

        protected abstract TOut Default(TIn input);
    }

    public abstract class MappingFunction<T1, T2, TOut>
    {
        private Func<T1, T2, TOut> _mapper;
        public Func<T1, T2, TOut> Mapper
        {
            private get
            {
                if (this._mapper == null)
                    return this.Default;

                return _mapper;
            }
            set
            {
                this._mapper = value;
            }
        }

        public TOut Map(T1 input1, T2 input2)
        {
            return Mapper(input1, input2);
        }

        protected abstract TOut Default(T1 input1, T2 input2);
    }
}
