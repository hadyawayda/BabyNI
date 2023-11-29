import { Fragment, useState } from 'react'
import { ReactChange, ReactEvent } from './Interfaces/Interfaces'
import { Dialog, Transition } from '@headlessui/react'

const GroupingSelector = ({
   onGroupingChange,
   onDateTimeKeySelect,
   grouping,
   dateTimeKeys,
}: {
   onGroupingChange: (grouping: ReactChange) => void
   onDateTimeKeySelect: (selection: ReactEvent) => void
   grouping: string
   dateTimeKeys: object
}) => {
   let [isOpen, setIsOpen] = useState(false)

   function handleGroupingChange(e: ReactChange) {
      onGroupingChange(e)
   }

   function handleDateTimeKeySelect(e: ReactChange) {
      onDateTimeKeySelect(e.target)
   }

   return (
      <>
         <button
            className="whitespace-nowrap underline"
            onClick={() => setIsOpen(true)}
         >
            Select Grouping
         </button>
         <Transition
            appear
            show={isOpen}
            as={Fragment}
         >
            <Dialog
               as="div"
               className="relative z-10"
               onClose={() => setIsOpen(false)}
            >
               <Transition.Child
                  as={Fragment}
                  enter="ease-out duration-300"
                  enterFrom="opacity-0"
                  enterTo="opacity-100"
                  leave="ease-in duration-200"
                  leaveFrom="opacity-100"
                  leaveTo="opacity-0"
               >
                  <div
                     className="fixed inset-0 bg-slate-950/60"
                     aria-hidden="true"
                  />
               </Transition.Child>
               <div className="fixed inset-0 overflow-y-auto">
                  <div className="flex min-h-full items-center justify-center p-4 text-center">
                     <Transition.Child
                        as={Fragment}
                        enter="ease-out duration-300"
                        enterFrom="opacity-0 scale-95"
                        enterTo="opacity-100 scale-100"
                        leave="ease-in duration-200"
                        leaveFrom="opacity-100 scale-100"
                        leaveTo="opacity-0 scale-95"
                     >
                        <Dialog.Panel className="text-black relative mx-auto p-9 rounded-2xl flex flex-col justify-start items-center bg-slate-100 dialog-panel">
                           <div className="flex flex-col justify-start items-center">
                              <Dialog.Title className="mb-4">
                                 Select KPIs:
                              </Dialog.Title>
                              <div className="flex justify-center w-96 gap-2 ml-4 mb-4">
                                 Grouping:
                                 <label>
                                    <input
                                       type="radio"
                                       name="grouping"
                                       value="NETYPE"
                                       className="whitespace-nowrap mr-4 align-middle"
                                       checked={grouping === 'NETYPE'}
                                       onChange={handleGroupingChange}
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
                                       onChange={handleGroupingChange}
                                    />
                                    NEALIAS
                                 </label>
                              </div>

                              <div className="mt-4">
                                 Select DateTime Keys:
                                 <div className=" flex flex-col justify-center gap-1.5 mt-4">
                                    {/* {dateTimeKeys.map((x) => (
                                    <div className="flex ml-4">
                                       <input
                                          type="checkbox"
                                          name={x.toString()}
                                          checked={Object.values(x)[0]}
                                          onChange={handleDateTimeKeySelect}
                                       />
                                       <label className="flex ml-8 text-black m-4">
                                          {Object.keys(x)}
                                       </label>
                                    </div>
                                 ))} */}
                                    {Object.entries(dateTimeKeys).map(
                                       ([key, value]) => (
                                          <div
                                             className="flex ml-4 h-8"
                                             key={key}
                                          >
                                             <input
                                                type="checkbox"
                                                name={key}
                                                checked={value}
                                                onChange={
                                                   handleDateTimeKeySelect
                                                }
                                             />
                                             <label className="ml-6 flex justify-center items-center">
                                                {key}
                                             </label>
                                          </div>
                                       )
                                    )}
                                 </div>
                              </div>
                           </div>
                        </Dialog.Panel>
                     </Transition.Child>
                  </div>
               </div>
            </Dialog>
         </Transition>
      </>
   )
}

export default GroupingSelector
