import Date from './Date'
import KPISelector from './KPISelector'
import { DateRange, ReactChange, ReactEvent } from './Interfaces/Interfaces'
import GroupingSelector from './GroupingSelector'

const Filters = ({
   onDateChange,
   onIntervalChange,
   onKPIChange,
   onGroupingChange,
   selectedKPIs,
   interval,
   dateTimeKeys,
   grouping,
}: {
   onDateChange: (date: DateRange) => void
   onIntervalChange: (interval: string) => void
   onKPIChange: (KPIs: ReactEvent) => void
   onGroupingChange: (item: ReactEvent) => void
   selectedKPIs: object
   interval: string
   dateTimeKeys: object
   grouping: string
}) => {
   function handleDateChange(date: DateRange) {
      onDateChange(date)
   }

   function handleIntervalChange(e: ReactChange) {
      onIntervalChange(e.target.value)
   }

   function handleKPIChange(KPI: ReactEvent) {
      onKPIChange(KPI)
   }

   function handleDateTimeKeySelect(selectedDateTimeKeys: any) {
      console.log(selectedDateTimeKeys)
   }

   function handleGroupingChange(e: ReactChange) {
      onGroupingChange(e.target)
   }

   return (
      <div className="flex gap-4 justify-around items-center px-28">
         <Date onDateChange={handleDateChange} />
         <div className="flex justify-center w-96 gap-2">
            Interval Aggregation:
            <label>
               <input
                  type="radio"
                  name="interval"
                  value="hourly"
                  className="mr-1 align-middle"
                  checked={interval === 'hourly'}
                  onChange={handleIntervalChange}
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
                  onChange={handleIntervalChange}
               />
               Daily
            </label>
         </div>
         <KPISelector
            onKPISelect={handleKPIChange}
            selectedKPIs={selectedKPIs}
         />
         <GroupingSelector
            onGroupingChange={handleGroupingChange}
            onDateTimeKeySelect={handleDateTimeKeySelect}
            {...{ grouping, dateTimeKeys }}
         />
      </div>
   )
}

export default Filters
