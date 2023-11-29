'use client'

import { useState } from 'react'
import { PageState, gridProps } from './Interfaces/Interfaces'
import { PagerTargetEvent } from '@progress/kendo-react-data-tools'
import {
   Grid,
   GridColumn as Column,
   GridPageChangeEvent,
} from '@progress/kendo-react-grid'

const Test = ({ data }: { data: gridProps }) => {
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
      <div>
         <Grid
            style={{ width: '100%', height: '400px' }}
            data={data.slice(page.skip, page.take + page.skip)}
            skip={page.skip}
            take={page.take}
            total={data.length}
            pageable={{
               buttonCount: 4,
               pageSizes: [5, 10, 15, 'All'],
               pageSizeValue: pageSizeValue,
            }}
            onPageChange={pageChange}
         >
            <Column
               field="TIME"
               title="TIME"
               width="170px"
               locked={true}
            />
            <Column
               field="TIME"
               title="TIME"
               width="170px"
               locked={true}
            />
            <Column
               field="TIME"
               title="TIME"
               width="170px"
               locked={true}
            />
            <Column
               field="TIME"
               title="TIME"
               width="170px"
               locked={true}
            />
         </Grid>
      </div>
   )
}

export default Test
