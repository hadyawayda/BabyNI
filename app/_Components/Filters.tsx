import Date from './Date'
import KPISelector from './KPISelector'
import { ReactChange, ReactEvent } from './Interfaces/Interfaces'

const Filters = ({
   onDateChange,
   onIntervalChange,
   onKPIChange,
   onGroupingChange,
   selectedKPIs,
}: {
   onDateChange: () => void
   onIntervalChange: (interval: string) => void
   onKPIChange: (KPIs: ReactEvent) => void
   onGroupingChange: (item: ReactEvent) => void
   selectedKPIs: object
}) => {
   function handleDateChange() {
      onDateChange()
   }

   function handleIntervalChange(e: ReactChange) {
      onIntervalChange(e.target.value)
   }

   function handleKPIChange(KPI: ReactEvent) {
      onKPIChange(KPI)
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
                  name="filter"
                  value="hourly"
                  className="mr-1 align-middle"
                  onChange={handleIntervalChange}
               />
               Hourly
            </label>
            <label>
               <input
                  type="radio"
                  name="filter"
                  value="daily"
                  className="mr-1 align-middle"
                  onChange={handleIntervalChange}
               />
               Daily
            </label>
         </div>
         <div className="flex">
            <p className="whitespace-nowrap mr-2">KPIs:</p>
            <KPISelector
               onKPISelect={handleKPIChange}
               selectedKPIs={selectedKPIs}
            />
         </div>
         <div className="flex justify-center w-96 gap-2 ml-4">
            Grouping:
            <label>
               <input
                  type="radio"
                  name="filter"
                  value="Both"
                  className="whitespace-nowrap mr-1 align-middle"
                  onChange={handleGroupingChange}
               />
               BOTH
            </label>
            <label>
               <input
                  type="radio"
                  name="filter"
                  value="NETYPE"
                  className="whitespace-nowrap mr-1 align-middle"
                  onChange={handleGroupingChange}
               />
               NETYPE
            </label>
            <label>
               <input
                  type="radio"
                  name="filter"
                  value="NEALIAS"
                  className="whitespace-nowrap mr-1 align-middle"
                  onChange={handleGroupingChange}
               />
               NEALIAS
            </label>
         </div>
      </div>
   )
}

export default Filters
