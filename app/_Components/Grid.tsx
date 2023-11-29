'use client'

import { useState } from 'react'
import { PageState, gridProps } from './Interfaces/Interfaces'
import { PagerTargetEvent } from '@progress/kendo-react-data-tools'
import {
   Grid as G,
   GridColumn as Column,
   GridPageChangeEvent,
} from '@progress/kendo-react-grid'

const Grid = ({
   data,
   selectedKPIs,
   grouping,
   dateTimeKeys,
}: {
   data: gridProps
   selectedKPIs: Map<string, boolean>
   grouping: string
   dateTimeKeys: object
}) => {
   const [page, setPage] = useState<PageState>({ skip: 0, take: 10 })
   const [pageSizeValue, setPageSizeValue] = useState<
      number | string | undefined
   >()
   const pageChange = (event: GridPageChangeEvent) => {
      const targetEvent = event.targetEvent as PagerTargetEvent
      const take = targetEvent.value === 'All' ? data.length : event.page.take

      if (targetEvent.value) {
         setPageSizeValue(targetEvent.value)
      }
      setPage({
         ...event.page,
         take,
      })
   }

   return (
      <div className="w-10/12 text-black">
         <div className="my-6 w-full flex justify-center">Performance Grid</div>
         <div>
            <G
               style={{
                  width: '100%',
                  height: '300px',
                  border: '1px solid #999',
               }}
               data={data.slice(page.skip, page.take + page.skip)}
               skip={page.skip}
               take={page.take}
               // filterable={true}
               pageable={{
                  buttonCount: 10,
                  info: true,
                  pageSizes: [10, 25, 50, 'All'],
                  pageSizeValue: pageSizeValue,
                  previousNext: true,
               }}
               reorderable={true}
               resizable={true}
               total={data.length}
               onPageChange={pageChange}
            >
               {/* <Column
                  field="DATETIME_KEY"
                  title="DATETIME_KEY"
                  width="130px"
               /> */}
               <Column
                  field="TIME"
                  title="TIME"
                  width="170px"
                  locked={true}
               />
               {['All', 'NETYPE'].includes(grouping) && (
                  <Column
                     field="NETYPE"
                     title="NETYPE"
                     width="130px"
                  />
               )}
               {['All', 'NEALIAS'].includes(grouping) && (
                  <Column
                     field="NEALIAS"
                     title="NEALIAS"
                     width="130px"
                  />
               )}
               {selectedKPIs.get('RSL_INPUT_POWER') ? (
                  <Column
                     field="RSL_INPUT_POWER"
                     title="RSL_INPUT_POWER"
                     width="170px"
                  />
               ) : null}
               {selectedKPIs.get('MAX_RX_LEVEL') ? (
                  <Column
                     field="MAX_RX_LEVEL"
                     title="MAX_RX_LEVEL"
                     width="140px"
                  />
               ) : null}
               {selectedKPIs.get('RSL_DEVIATION') ? (
                  <Column
                     field="RSL_DEVIATION"
                     title="RSL_DEVIATION"
                     width="140px"
                  />
               ) : null}
            </G>
         </div>
      </div>
   )
}

export default Grid
