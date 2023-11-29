import { ReactChange } from './Interfaces/Interfaces'

const GroupingSelector = ({
   onGroupingChange,
   grouping,
}: {
   onGroupingChange: (grouping: ReactChange) => void
   grouping: string
}) => {
   return (
      <>
         <div className="flex justify-center w-96 gap-2 ml-4 mb-4">
            Grouping:
            <label>
               <input
                  type="radio"
                  name="grouping"
                  value="NETYPE"
                  className="whitespace-nowrap mr-4 align-middle"
                  checked={grouping === 'NETYPE'}
                  onChange={onGroupingChange}
               />
               NETYPE
            </label>
            <label>
               <input
                  type="radio"
                  name="grouping"
                  value="NEALIAS"
                  className="whitespace-nowrap mr-4 align-middle"
                  checked={grouping === 'NEALIAS'}
                  onChange={onGroupingChange}
               />
               NEALIAS
            </label>
         </div>
      </>
   )
}

export default GroupingSelector
