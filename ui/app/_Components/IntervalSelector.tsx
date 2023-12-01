import { IntervalProps } from './Interfaces/Interfaces'

const Interval = ({ interval, onIntervalChange }: IntervalProps) => {
   return (
      <div className="flex justify-center gap-2">
         Interval Aggregation:
         <label>
            <input
               type="radio"
               name="interval"
               value="hourly"
               className="mr-1 align-middle"
               checked={interval === 'hourly'}
               onChange={onIntervalChange}
            />
            Hourly
         </label>
         <label>
            <input
               type="radio"
               name="interval"
               value="daily"
               className="mr-1 align-middle"
               checked={interval === 'daily'}
               onChange={onIntervalChange}
            />
            Daily
         </label>
      </div>
   )
}

export default Interval
